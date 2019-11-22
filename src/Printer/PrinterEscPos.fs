namespace Printer

open System.Runtime.ConstrainedExecution
open System.Threading.Tasks
open Interfaces
open CommonLibrary.Conversions

type DocumentDefinition = {
     ColsNormal: int
     ColsCondensed: int
     ColsExpanded: int
    }

type PrinterEscPos(docDef:DocumentDefinition) =
    
    let dashLine cols = String.replicate cols "-"
    
    let isCondensed c =
        match c with
        | Some true -> true
        | _ -> false
    
    let getNTimes cols condensed =
        let b = isCondensed condensed
        let nTimes =
            match cols with
            | Some x when x > 0 -> x
            | _ ->  if b then docDef.ColsCondensed - 2 else docDef.ColsNormal - 2
        (b, nTimes)
        
    let prepareTable sep condensed =
        let times = getNTimes None condensed           
        (fst times, sep + dashLine (snd times) + sep )
                
    member this.Commands = (this :> ICommandEscPos)    
   
    interface ICommandEscPos with
        
        member this.Bold(arg1: OnOff): byte array = 
            match arg1 with
            | On -> [|27; 69; 1|] 
            | Off -> [|27; 69; 0|]
            |> ToByteArray

        member this.BoldText(arg1: string): byte array = 
            [ this.Commands.Bold(On) ; 
              ToByte(arg1) ; 
              this.Commands.Bold(Off) ;
              ToByte("\n")]
            |> Array.concat

        member this.Expanded(arg1: OnOff): byte array = 
            match arg1 with
            | On -> [|29; 33; 16|] 
            | Off -> [|29; 33; 0|]
            |> ToByteArray

        member this.ExpandedText(arg1: string): byte array = 
            [ this.Commands.Expanded(On) ; 
              ToByte(arg1) ; 
              this.Commands.Expanded(Off) ;
              ToByte("\n")]
            |> Array.concat 

        member this.Condensed(arg1: OnOff): byte array = 
            match arg1 with
            | On -> [|27; 33; 1|] 
            | Off -> [|27; 33; 0|]
            |> ToByteArray

        member this.CondensedText(arg1: string): byte array = 
            [ this.Commands.Condensed(On) ; 
              ToByte(arg1) ; 
              this.Commands.Condensed(Off) ;
              ToByte("\n")]
            |> Array.concat

        member this.Underline(arg1: OnOff): byte array = 
            match arg1 with
            | On -> [|27; 45; 1|] 
            | Off -> [|27; 45; 0|]
            |> ToByteArray

        member this.UnderlineText(arg1: string): byte array = 
            [ this.Commands.Underline(On) ; 
              ToByte(arg1) ; 
              this.Commands.Underline(Off);
              ToByte("\n")]
            |> Array.concat

        member this.OpenCashDrawer: byte array = 
            ToByteArray [|27; 112; 0; 60; 120|]  

        member this.Italic(arg1:OnOff): byte array = 
            match arg1 with
            | On -> [|27; 52|] 
            | Off -> [|27; 53|]
            |> ToByteArray

        member this.ItalicText(arg1:string): byte array =
            [ this.Commands.Italic(On) ; 
              ToByte(arg1) ; 
              this.Commands.Italic(Off);
              ToByte("\n")]
            |> Array.concat

        member this.DoubleStrike(arg1:OnOff): byte array = 
            match arg1 with
            | On -> [|27; 71; 1|] 
            | Off -> [|27; 71; 0|]
            |> ToByteArray

        member this.DoubleStrikeText(arg1:string): byte array =
            [ this.Commands.DoubleStrike(On) ; 
              ToByte(arg1) ; 
              this.Commands.DoubleStrike(Off);
              ToByte("\n")]
            |> Array.concat

        member this.Invert(arg1: OnOff): byte array = 
            match arg1 with
            | On -> [|29; 66; 1|] 
            | Off -> [|29; 66; 0|]
            |> ToByteArray

        member this.InvertText(arg1: string): byte array = 
            [ this.Commands.Invert(On) ; 
              ToByte(arg1) ; 
              this.Commands.Invert(Off);
              ToByte("\n")]
            |> Array.concat
            
        member this.FontA(_:OnOff): byte array = 
            ToByteArray [|27; 77; 48|] 
            
        member this.FontAText(arg1:string): byte array =
            [ this.Commands.FontA(On) ; 
              ToByte(arg1) ; 
              this.Commands.FontA(Off);
              ToByte("\n")]
            |> Array.concat

        member this.FontB(arg1:OnOff): byte array = 
            match arg1 with
            | On -> [|27; 77; 49|] 
            | Off -> [|27; 77; 48|]
            |> ToByteArray
            
        member this.FontBText(arg1:string): byte array =
            [ this.Commands.FontB(On) ; 
              ToByte(arg1) ; 
              this.Commands.FontB(Off);
              ToByte("\n")]
            |> Array.concat
            
        member this.Left: byte array = 
            ToByteArray [|27; 97; 0|] 

        member this.Center: byte array = 
            ToByteArray [|27; 97; 1|] 

        member this.Right: byte array = 
            ToByteArray [|27; 97; 2|] 

        member this.PaperCut(arg1:CutType) : byte array =
            match arg1 with
            | Partial -> [|29; 86; 65; 3|] 
            | Full -> [|29; 86; 65; 3|]
            |> ToByteArray

        member this.AutoTest: byte array =
            ToByteArray [|29; 40; 65; 2; 0; 0; 2|] 

        member this.InitializePrinter: byte array =
            ToByteArray [|27; 64|]
        
        member this.Separator(?cols:int): byte array =
            let nTimes = match getNTimes cols (Some false) with | _, n -> n                
            this.Commands.FontAText(dashLine nTimes)

        member this.SeparatorCondensed(?cols:int): byte array =
            let nTimes = match getNTimes cols (Some true) with | _, n -> n                           
            this.Commands.CondensedText(dashLine nTimes)  
        
        member this.OpenCloseTable(?condensed:bool): byte array =
            match prepareTable "+" condensed with
            | true, s ->  
                this.Commands.CondensedText(s)
            | _, s ->
                this.Commands.FontAText(s)                 
            
        member this.LineTable(?condensed:bool): byte array =
            match prepareTable "|" condensed with
            | true, s ->  
                this.Commands.CondensedText(s)
            | _, s ->
                this.Commands.FontAText(s)