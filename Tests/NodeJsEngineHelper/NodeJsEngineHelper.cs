﻿//MIT, 2019-present, WinterDev 
using System;
using System.Collections.Generic;

namespace Espresso
{
    public delegate string LoadMainSrcFile();
    public delegate string NodeJsExecSessionSetup(NodeJsExecSession session);

    public class NodeJsExecSession
    {
        public readonly JsEngine Engine;
        public readonly JsContext Context;
        public readonly JsTypeDefinition LibEspressoClass;

        //internal LoadMainSrcFile _loadMainSrcFile;
        internal NodeJsExecSession(JsEngine engine, JsContext ctx, JsTypeDefinition libEspressoClass)
        {
            Engine = engine;
            Context = ctx;
            LibEspressoClass = libEspressoClass;
        }
        //public void SetMainSrcFile(LoadMainSrcFile loadMainSrcFile)
        //{
        //    _loadMainSrcFile = loadMainSrcFile;
        //}
        public void SetExternalObj<T>(string name, T obj) where T : class
        {
            Context.SetVariableAutoWrap<T>(name, obj);
        }
    }

    public static class NodeJsEngineHelper
    {
        public static void Run(NodeJsExecSessionSetup nodeExecSession)
        {
            //------------ 
            NodeJsEngine.Run((eng, ctx) =>
            {
                //-------------
                //this LibEspressoClass object is need,
                //so node can talk with us,
                //-------------

                JsTypeDefinition jstypedef = new JsTypeDefinition("LibEspressoClass");
                NodeJsExecSession nodeJsExecSession = new NodeJsExecSession(eng, ctx, jstypedef);
                string mainSrc = nodeExecSession(nodeJsExecSession);

                jstypedef.AddMember(new JsMethodDefinition("LoadMainSrcFile", args =>
                { 
                    args.SetResult(mainSrc); 
                }));
                if (!jstypedef.IsRegisterd)
                {
                    ctx.RegisterTypeDefinition(jstypedef);
                }
                //----------
                //then register this as x***       
                //this object is just an instance for reference        
                ctx.SetVariableFromAny("LibEspresso", ctx.CreateWrapper(new object(), jstypedef));
            });
        }
        public static void Run(LoadMainSrcFile loadMainSrcFile)
        {
            //------------ 
            NodeJsEngine.Run((eng, ctx) =>
            {
                //-------------
                //this LibEspressoClass object is need,
                //so node can talk with us,
                //-------------
                JsTypeDefinition jstypedef = new JsTypeDefinition("LibEspressoClass");
                jstypedef.AddMember(new JsMethodDefinition("LoadMainSrcFile", args =>
                {
                    args.SetResult(loadMainSrcFile());
                }));
                if (!jstypedef.IsRegisterd)
                {
                    ctx.RegisterTypeDefinition(jstypedef);
                }
                //----------
                //then register this as x***       
                //this object is just an instance for reference        
                ctx.SetVariableFromAny("LibEspresso", ctx.CreateWrapper(new object(), jstypedef));
            });

        }
    }
}