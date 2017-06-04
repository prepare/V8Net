// This file is part of the VroomJs library.
//
// Author:
//     Federico Di Gregorio <fog@initd.org>
//
// Copyright © 2013 Federico Di Gregorio <fog@initd.org>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Runtime.InteropServices;

namespace Espresso
{

    //---------------------------------------
    //2017-06-04
    //1. for internal inter-op only -> always be private
    //for inter-op with native lib, .net core on macOS x64 dose not support explicit layout
    //so we need sequential layout
    //2. this is a quite large object, and is designed to be used on stack,
    //pass by reference to native side
    //---------------------------------------
    [StructLayout(LayoutKind.Sequential)]
    struct JsInterOpValue
    {
        public int I32;//4
        public long I64;//8
        public double Num;//8
        /// <summary>
        /// native ptr
        /// </summary>
        public IntPtr Ptr;//8 on 64 bits
        //type
        public JsValueType Type; //4
        //len of string and array
        public int Length;
        /// <summary>
        /// index to managed slot
        /// </summary>
        public int Index;
    }
}
