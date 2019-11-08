namespace RawPrinterHelper

module Printer =
    open System
    open System.IO
    open System.Runtime.InteropServices
    open Native

    let randomStr = 
        let chars = "ABCDEFGHIJKLMNOPQRSTUVWUXYZ0123456789"
        let charsLen = chars.Length
        let random = System.Random()

        fun len -> 
            let randomChars = [|for i in 0..len -> chars.[random.Next(charsLen)]|]
            System.String(randomChars)

    let private sendToPrinter szPrinterName pBytes dwCount =
        let mutable dwError = 0
        let mutable dwWritten = 0
        let mutable hPrinter = new nativeint(0)
        let mutable di = DOCINFOA(pDocName = randomStr(8), pDataType = "RAW", pOutputFile = null)
        let mutable bSuccess = false

        match OpenPrinter(szPrinterName, hPrinter, IntPtr.Zero) with
        | true ->   
            match StartDocPrinter(hPrinter, 1, di) with
            | true ->
                let _ = 
                    match StartPagePrinter(hPrinter) with
                    | true -> 
                        bSuccess <- WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
                        EndPagePrinter(hPrinter)
                    | _ -> false
                
                EndDocPrinter(hPrinter)
                |> ignore

            | _ -> ()

            ClosePrinter(hPrinter)
            |> ignore
        | _ -> ()

        if bSuccess then
            dwError <- Marshal.GetLastWin32Error()        
        bSuccess
 
    
    let SendBytesToPrinter (szPrinterName: string, data: byte[]) =
        let len = Array.length data
        let pUnmanagedBytes = Marshal.AllocCoTaskMem(Array.length data)
        Marshal.Copy(data, 0, pUnmanagedBytes, len)   

        let retVal = sendToPrinter szPrinterName pUnmanagedBytes len
        
        if len > 0 then
            Marshal.FreeCoTaskMem(pUnmanagedBytes)        
        retVal

    let SendStringToPrinter (szPrinterName: string, szString: string) =
        // How many characters are in the string?
        let dwCount = szString.Length
        
        // Assume that the printer is expecting ANSI text, and then convert
        // the string to ANSI text.
        let pBytes = Marshal.StringToCoTaskMemAnsi(szString)

        // Send the converted ANSI string to the printer.
        let retVal = sendToPrinter szPrinterName pBytes dwCount
        retVal
    
    let SendAsciiToPrinter (szPrinterName: string, data: string) =
        let enc = System.Text.Encoding.ASCII.GetBytes data
        //if  you are using UTF-8 and get wrong values in qrcode printing, you must use ASCII instead.
        //let enc = System.Text.Encoding.UTF8.GetBytes(data)
        
        let retVal = SendBytesToPrinter(szPrinterName, enc)
        retVal

    let SendFileToPrinter (szPrinterName: string, szFileName: string) =
        use stream = File.Open(szFileName, FileMode.Open, FileAccess.Read)
        use mem = new MemoryStream()
        stream.CopyTo mem
        let data = mem.ToArray()
        let len = Array.length data

        // Allocate some unmanaged memory for those bytes.
        let pUnmanagedBytes = Marshal.AllocCoTaskMem(len)
        Marshal.Copy(data, 0, pUnmanagedBytes, len)   
        let retVal = sendToPrinter szPrinterName pUnmanagedBytes len

        if len > 0 then
            Marshal.FreeCoTaskMem(pUnmanagedBytes) 
        retVal
         




        
    

