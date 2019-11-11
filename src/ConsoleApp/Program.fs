open System
open RawPrinterHelper.Printer 
open CommonLibrary.SysOp

let getInput () =
    printf "print this:>"
    Console.ReadLine ()

let output (s:string) =
    printfn "You typed: %s" s
    SendStringToPrinter("PDF", s) |> ignore

let rec gameLoop() =
    let input = getInput ()
    output input
    match input with
    | "" -> ()
    | _ -> gameLoop() 

[<EntryPoint>]
let main argv =
    printfn "Printing..."

    match getOS with
    | Linux -> printfn "Linux"
    | Windows -> printfn "Windows"
    | _ -> printfn "outro"

    //SendStringToPrinter("PDF", "teste de impressao") |> ignore

    //gameLoop
    //|> ignore

    //let proc = 
    //    for x in argv do
    //        SendStringToPrinter("PDF", x) |> ignore

    1