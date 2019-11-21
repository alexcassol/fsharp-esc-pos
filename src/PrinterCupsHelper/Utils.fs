namespace PrinterCupsHelper

module private Utils =

    type TempFile() =
        let path = System.IO.Path.GetTempFileName()
        member x.Path = path

        member x.Write (b:byte array) =
            System.IO.File.WriteAllBytes (x.Path, b)
        
        interface System.IDisposable with
            member x.Dispose() = System.IO.File.Delete(path)