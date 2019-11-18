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
        abstract member Left : unit -> byte array
        abstract member Right : unit -> byte array
        abstract member Center : unit -> byte array

    type ICommandEscPos =
        inherit IFontMode
        inherit IAlignment

        abstract member InitializePrinter: unit -> byte array

        abstract member FontA: OnOff -> byte array
        abstract member FontAText: string -> byte array
        abstract member FontB: OnOff -> byte array
        abstract member FontBText: string -> byte array

        abstract member OpenCashDrawer: unit -> byte array
        abstract member PaperCut: CutType -> byte array
        abstract member AutoTest: unit -> byte array

    type ICommandEPL =
        inherit IFontMode
        inherit IAlignment
 