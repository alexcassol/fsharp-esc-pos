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
 
 

         
             