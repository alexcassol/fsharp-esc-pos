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

module Interfaces =
    type OnOff = | On | Off
    
    type CutType = | Partial | Full

    type IFontMode =
        abstract member Italic  : OnOff -> byte array
        abstract member ItalicText: string -> byte array
        abstract member Bold : OnOff -> byte array
        abstract member BoldText : string -> byte array
        abstract member Underline : OnOff -> byte array
        abstract member UnderlineText : string -> byte array
        abstract member Expanded : OnOff -> byte array
        abstract member ExpandedText : string -> byte array
        abstract member Condensed : OnOff -> byte array
        abstract member CondensedText : string -> byte array
        abstract member DoubleStrike : OnOff -> byte array
        abstract member DoubleStrikeText : string -> byte array
        abstract member Invert : OnOff -> byte array
        abstract member InvertText: string -> byte array
    
    type IAlignment =
        abstract member Left : byte array with get
        abstract member Right : byte array with get
        abstract member Center : byte array with get

    type ICommandEscPos =
        inherit IFontMode
        inherit IAlignment

        abstract member InitializePrinter: byte array with get
        abstract member FontA: OnOff -> byte array
        abstract member FontAText: string -> byte array
        abstract member FontB: OnOff -> byte array
        abstract member FontBText: string -> byte array
        abstract member OpenCashDrawer: byte array with get
        abstract member PaperCut: CutType -> byte array
        abstract member AutoTest: byte array with get
        abstract member Separator: ?cols:int  -> byte array
        abstract member SeparatorCondensed: ?cols:int -> byte array
        abstract member OpenCloseTable: ?condensed:bool -> byte array
        abstract member LineTable: ?condensed:bool -> byte array
        
    type ICommandEPL =
        inherit IFontMode
        inherit IAlignment
 