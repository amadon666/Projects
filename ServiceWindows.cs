{$reference 'System.ServiceProcess.dll'}
uses System,System.ServiceProcess, System.IO,System.Threading;

type Logger = class
   public watcher: FileSystemWatcher;
          obj:= new object();
          enabled:= true;
          
   public constructor();
       begin
         watcher:= new FileSystemWatcher('C:\Users\fdshfgas\Desktop');
         watcher.Deleted += Watcher_Deleted;
         watcher.Created += Watcher_Created;
         watcher.Changed += Watcher_Changed;
         watcher.Renamed += Watcher_Renamed;
       end;
       
   public procedure Start();
       begin
         watcher.EnableRaisingEvents:= true;
         
         while (enabled)do begin
            Thread.Sleep(1000);
         end;
       end;
       
   public procedure Stop();
       begin
          watcher.EnableRaisingEvents:= false;
          enabled:= false;
       end;

   public procedure Watcher_Deleted(sender: object;e: FileSystemEventArgs);
      begin
        var fileEvent: string:= 'удален ';
        var filePath:= e.FullPath;
        RecordEntry(fileEvent, filePath);
      end;
      
   public procedure Watcher_Created(sender: object;e: FileSystemEventArgs);
      begin
        var fileEvent: string:= 'создан ';
        var filePath:= e.FullPath;
        RecordEntry(fileEvent, filePath);
      end;
      
   public procedure Watcher_Changed(sender: object;e: FileSystemEventArgs);
      begin
        var fileEvent: string:= 'изменен ';
        var filePath:= e.FullPath;
        RecordEntry(fileEvent, filePath);
      end;
      
   public procedure Watcher_Renamed(sender: object;e: RenamedEventArgs);
      begin
         var fileEvent: string:= 'переименован в ' + e.FullPath;
         var filePath:= e.OldFullPath;
         RecordEntry(fileEvent, filePath);
      end;    
       
   private procedure RecordEntry(fileEvent, filePath: string);
     begin
       lock obj do begin
           var writer:= new StreamWriter('C:\Users\fdshfgas\Desktop\Inf.txt');
           
           writer.WriteLine(String.Format('{0} Файл {1} был {2}',
            DateTime.Now.ToString(), filePath, fileEvent));
           writer.Flush();
           writer.Close();
       end;
     end;

end;

type Service = class(ServiceBase)
   public logg: Logger:= new Logger();
   
   public constructor ();
      begin
        self.CanStop:= true;// службу можно остановить
        self.CanPauseAndContinue:= true;// службу можно приостановить и затем продолжить
        self.AutoLog:= true;// служба может вести запись в лог
      end;
      
   public procedure OnStart();
      begin
         logg:= new Logger();
         var loggThread:= new Thread(logg.Start);
         loggThread.Start();
      end;
      
   public procedure OnStop();override;   
      begin
        logg.Stop();
        Thread.Sleep(1000);
      end;
   
end;

var serv:= new Service();
begin
  serv.OnStart();
end.
