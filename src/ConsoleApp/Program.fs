open System
open RawPrinterHelper.Printer

let getInput () =
    printf "print this:>"
    Console.ReadLine ()

let output (s:string) =
    printfn "You typed: %s" s
    SendStringToPrinter("PDF", s) |> ignore

let rec gameLoop() =
    let input = getInput ()
    output input
    gameLoop() 

[<EntryPoint>]
let main argv =
    printfn "Printing..."

    let proc = 
        for x in argv do
            SendStringToPrinter("PDF", x) |> ignore

    1