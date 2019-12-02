//The MIT License (MIT)
//Copyright (c) 2019 Alex Cassol
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights to
//use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//the Software, and to permit persons to whom the Software is furnished to do so,
//subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
//FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//DEALINGS IN THE SOFTWARE.

namespace Printer
open Interfaces

type PrinterEPL() = 

    interface ICommandEPL with
        member this.Invert(arg1: OnOff): byte array = 
            failwith "Not Implemented"
        member this.InvertText(arg1: string): byte array = 
            failwith "Not Implemented"

        member this.Center: byte array = 
            failwith "Not Implemented"
        member this.Left: byte array = 
            failwith "Not Implemented"
        member this.Right: byte array = 
            failwith "Not Implemented"

        member this.Bold(arg1: OnOff): byte array = 
            failwith "Not Implemented"
        member this.BoldText(arg1: string): byte array = 
            failwith "Not Implemented"
        member this.Condensed(arg1: OnOff): byte array = 
            failwith "Not Implemented"
        member this.CondensedText(arg1: string): byte array = 
            failwith "Not Implemented"
        member this.DoubleStrike(arg1: OnOff): byte array = 
            failwith "Not Implemented"
        member this.DoubleStrikeText(arg1: string): byte array = 
            failwith "Not Implemented"
        member this.Expanded(arg1: OnOff): byte array = 
            failwith "Not Implemented"
        member this.ExpandedText(arg1: string): byte array = 
            failwith "Not Implemented"
        member this.Italic(arg1: OnOff): byte array = 
            failwith "Not Implemented"
        member this.ItalicText(arg1: string): byte array = 
            failwith "Not Implemented"
        member this.Underline(arg1: OnOff): byte array = 
            failwith "Not Implemented"
        member this.UnderlineText(arg1: string): byte array = 
            failwith "Not Implemented"
 
 

         
             