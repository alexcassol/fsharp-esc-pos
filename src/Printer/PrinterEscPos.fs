namespace Printer
open Interfaces
open CommonLibrary.Conversions


type DocumentDefinition = { 
    ColsNormal: int
    ColsCondensed: int
    ColsExpanded: int 
    }    

type PrinterEscPos(docDef:DocumentDefinition) = 

    member this.DocLanguage =
        this :> ICommandEscPos
 
    interface ICommandEscPos with
        
        member this.Bold(arg1: OnOff): byte array = 
            match arg1 with
            | On -> ToByteArray [|27; 69; 1|] 
            | Off -> ToByteArray [|27; 69; 0|] 

        member this.BoldText(arg1: string): byte array = 
            Array.concat [  
                (this :> ICommandEscPos).Bold(On) ; 
                ToByte(arg1) ; 
                (this :> ICommandEscPos).Bold(Off) ;
                ToByte("\n")]

        member this.Expanded(arg1: OnOff): byte array = 
            match arg1 with
            | On -> ToByteArray [|29; 33; 16|] 
            | Off -> ToByteArray [|29; 33; 0|] 

        member this.ExpandedText(arg1: string): byte array = 
            Array.concat [  
                (this :> ICommandEscPos).Expanded(On) ; 
                ToByte(arg1) ; 
                (this :> ICommandEscPos).Expanded(Off) ;
                ToByte("\n")]

        member this.Condensed(arg1: OnOff): byte array = 
            match arg1 with
            | On -> ToByteArray [|27; 33; 1|] 
            | Off -> ToByteArray [|27; 33; 0|] 

        member this.CondensedText(arg1: string): byte array = 
            Array.concat [  
                (this :> ICommandEscPos).Condensed(On) ; 
                ToByte(arg1) ; 
                (this :> ICommandEscPos).Condensed(Off) ;
                ToByte("\n")]

        member this.Underline(arg1: OnOff): byte array = 
            match arg1 with
            | On -> ToByteArray [|27; 45; 1|] 
            | Off -> ToByteArray [|27; 45; 0|] 

        member this.UnderlineText(arg1: string): byte array = 
            Array.concat [  
                (this :> ICommandEscPos).Underline(On) ; 
                ToByte(arg1) ; 
                (this :> ICommandEscPos).Underline(Off);
                ToByte("\n")]

        member this.OpenCashDrawer(): byte array = 
            ToByteArray [|27; 112; 0; 60; 120|]  

        member this.Italic(arg1:OnOff): byte array = 
            match arg1 with
            | On -> ToByteArray [|27; 52|] 
            | Off -> ToByteArray [|27; 53|] 

        member this.ItalicText(arg1:string): byte array =
            Array.concat [  
                (this :> ICommandEscPos).Italic(On) ; 
                ToByte(arg1) ; 
                (this :> ICommandEscPos).Italic(Off);
                ToByte("\n")]

        member this.DoubleStrike(arg1:OnOff): byte array = 
            match arg1 with
            | On -> ToByteArray [|27; 71; 1|] 
            | Off -> ToByteArray [|27; 71; 0|] 

        member this.DoubleStrikeText(arg1:string): byte array =
            Array.concat [  
                (this :> ICommandEscPos).DoubleStrike(On) ; 
                ToByte(arg1) ; 
                (this :> ICommandEscPos).DoubleStrike(Off);
                ToByte("\n")]

        member this.Invert(arg1: OnOff): byte array = 
            match arg1 with
            | On -> ToByteArray [|29; 66; 1|] 
            | Off -> ToByteArray [|29; 66; 0|] 

        member this.InvertText(arg1: string): byte array = 
            Array.concat [  
                (this :> ICommandEscPos).Invert(On) ; 
                ToByte(arg1) ; 
                (this :> ICommandEscPos).Invert(Off);
                ToByte("\n")]
        
        member this.FontA(_:OnOff): byte array = 
            ToByteArray [|27; 77; 48|] 
            
        member this.FontAText(arg1:string): byte array =
            Array.concat [  
                (this :> ICommandEscPos).FontA(On) ; 
                ToByte(arg1) ; 
                (this :> ICommandEscPos).FontA(Off);
                ToByte("\n")]

        member this.FontB(arg1:OnOff): byte array = 
            match arg1 with
            | On -> ToByteArray [|27; 77; 49|] 
            | Off -> ToByteArray [|27; 77; 48|] 
            
        member this.FontBText(arg1:string): byte array =
            Array.concat [  
                (this :> ICommandEscPos).FontB(On) ; 
                ToByte(arg1) ; 
                (this :> ICommandEscPos).FontB(Off);
                ToByte("\n")]

        member this.Left(): byte array = 
            ToByteArray [|27; 97; 0|] 

        member this.Center(): byte array = 
            ToByteArray [|27; 97; 1|] 

        member this.Right(): byte array = 
            ToByteArray [|27; 97; 2|] 

        member this.PaperCut(arg1:CutType) : byte array =
            match arg1 with
            | Partial -> ToByteArray [|29; 86; 65; 3|] 
            | Full -> ToByteArray [|29; 86; 65; 3|] 

        member this.AutoTest() : byte array =
            ToByteArray [|29; 40; 65; 2; 0; 0; 2|] 

        member this.InitializePrinter() : byte array =
            ToByteArray [|27; 64|] 