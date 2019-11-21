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

namespace RawPrinterHelper

module Printer =
    open System
    open System.IO
    open System.Runtime.InteropServices
    open Native
    open CommonLibrary.Utils

    let getError =
        Marshal.GetLastWin32Error()  

    let private sendToPrinter szPrinterName pBytes dwCount =
        let mutable dwWritten = 0
        let mutable hPrinter = new nativeint(0)
        let mutable di = DOCINFOA(pDocName = randomStr 8, pDataType = "RAW", pOutputFile = null)
        let mutable bSuccess = false

        if OpenPrinter(szPrinterName, hPrinter, IntPtr.Zero) then
            if StartDocPrinter(hPrinter, 1, di) then
                if StartPagePrinter(hPrinter) then
                    if WritePrinter(hPrinter, pBytes, dwCount, dwWritten) then
                        bSuccess <- true
                    EndPagePrinter(hPrinter) |> ignore                        
                EndDocPrinter(hPrinter) |> ignore 
            ClosePrinter(hPrinter) |> ignore 

        match bSuccess with
        | true -> 0
        | _ ->  getError
 
    
    let SendBytesToPrinter (szPrinterName: string, data: byte[]) =
        let len = Array.length data
        let pUnmanagedBytes = Marshal.AllocCoTaskMem(Array.length data)
        Marshal.Copy(data, 0, pUnmanagedBytes, len)   

        let retVal = sendToPrinter szPrinterName pUnmanagedBytes len
        
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
    
    let SendAsciiToPrinter (szPrinterName: string, szString: string) =
        let enc = System.Text.Encoding.ASCII.GetBytes szString
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

        Marshal.FreeCoTaskMem(pUnmanagedBytes) 
        retVal
         




        
    

