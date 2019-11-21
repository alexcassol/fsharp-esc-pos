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

    type ICommandEPL =
        inherit IFontMode
        inherit IAlignment
 