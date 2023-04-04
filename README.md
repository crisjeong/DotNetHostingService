# DotNetHostingService
1. .NET Generic Hosted Console Application
2. Use Serilog
3. Use Coravel for scheduler
   > Coravel's Scheduling service is attempting to close but there are tasks still running.
   > App closing (in background) will be prevented until all tasks are completed.

   > Cancel Long-Running Invocables   
   > Make your long-running invocable classes implement Coravel.Invocable.ICancellableInvocable to enable it to gracefully abort on application shutdown.   
   > The interface includes a property CancellationToken that you can check using CancellationToken.IsCancellationRequested
