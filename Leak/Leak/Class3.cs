
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// Design pattern for a base class.
public abstract class Base : IDisposable
{
    private bool disposed = false;
    private string instanceName;
    private List<object> trackingList;

    public Base(string instanceName, List<object> tracking)
    {
        this.instanceName = instanceName;
        trackingList = tracking;
        trackingList.Add(this);
    }

    public string InstanceName
    {
        get
        {
            return instanceName;
        }
    }

    //Implement IDisposable.
    public void Dispose()
    {
        Console.WriteLine("\n[{0}].Base.Dispose()", instanceName);
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Free other state (managed objects).
                Console.WriteLine("[{0}].Base.Dispose(true)", instanceName);
                trackingList.Remove(this);
                Console.WriteLine("[{0}] Removed from tracking list: {1:x16}",
                    instanceName, this.GetHashCode());
            }
            else
            {
                Console.WriteLine("[{0}].Base.Dispose(false)", instanceName);
            }
            disposed = true;
        }
    }

    // Use C# destructor syntax for finalization code.
    ~Base()
    {
        // Simply call Dispose(false).
        Console.WriteLine("\n[{0}].Base.Finalize()", instanceName);
        Dispose(false);
    }
}

// Design pattern for a derived class.
public class Derived : Base
{
    private bool disposed = false;
    private IntPtr umResource;

    public Derived(string instanceName, List<object> tracking) :
        base(instanceName, tracking)
    {
        // Save the instance name as an unmanaged resource
        umResource = Marshal.StringToCoTaskMemAuto(instanceName);
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Console.WriteLine("[{0}].Derived.Dispose(true)", InstanceName);
                // Release managed resources.
            }
            else
            {
                Console.WriteLine("[{0}].Derived.Dispose(false)", InstanceName);
            }
            // Release unmanaged resources.
            if (umResource != IntPtr.Zero)
            {
                //creating memory leak
               // Marshal.FreeCoTaskMem(umResource);
               
                Console.WriteLine("[{0}] Unmanaged memory freed at {1:x16}",
                    InstanceName, umResource.ToInt64());
                umResource = IntPtr.Zero;
            }
            disposed = true;
        }
        // Call Dispose in the base class.
        base.Dispose(disposing);
    }
    // The derived class does not have a Finalize method
    // or a Dispose method without parameters because it inherits
    // them from the base class.
}
