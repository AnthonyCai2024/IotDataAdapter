using System.IO.BACnet;
using Mtim.Grpc.BACnet.Models;

namespace Mtim.Grpc.BACnet.Services;

public static class BACnetService
{
    static BacnetClient bacnet_client;

    // All the present Bacnet Device List
    static List<BacNode> DevicesList = new List<BacNode>();

    public static bool ReadExample()
    {
        bacnet_client = new BacnetClient(new BacnetIpUdpProtocolTransport(0xBAC0, false));

        bacnet_client.Start(); // go

        bacnet_client.WhoIs();


        var bacnetAddress = new BacnetAddress(BacnetAddressTypes.IP, "127.0.0.1");

        Thread.Sleep(2000);

        var res = bacnet_client.ReadPropertyRequest(bacnetAddress,
            new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_INPUT, 0),
            BacnetPropertyIds.PROP_PRESENT_VALUE
            , out var Values);

        Console.WriteLine("Read value : " + Values[0].Value.ToString());

        return false;


      
    }

    public static void ReadWriteExample(int deviceId)
    {
        if (deviceId == 0)
        {
            deviceId = 12345;
        }

        BacnetValue Value;
        bool ret;
        // Read Present_Value property on the object ANALOG_INPUT:0 provided by the device 12345
        // Scalar value only
        ret = ReadScalarValue(deviceId, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_INPUT, 0),
            BacnetPropertyIds.PROP_PRESENT_VALUE, out Value);

        if (ret == true)
        {
            Console.WriteLine("Read value : " + Value.Value.ToString());

            // Write Present_Value property on the object ANALOG_OUTPUT:0 provided by the device 4000
            BacnetValue newValue = new BacnetValue(Convert.ToSingle(Value.Value)); // expect it's a float
            // ret = WriteScalarValue(4000, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_OUTPUT, 0), BacnetPropertyIds.PROP_PRESENT_VALUE, newValue);

            Console.WriteLine("Write feedback : " + ret.ToString());
        }
        else
            Console.WriteLine("Error somewhere !");
    }

    static bool ReadScalarValue(int device_id, BacnetObjectId BacnetObjet, BacnetPropertyIds Propriete,
        out BacnetValue Value)
    {
        BacnetAddress adr;
        IList<BacnetValue> NoScalarValue;

        Value = new BacnetValue(null);

        // Looking for the device
        adr = DeviceAddr((uint)device_id);
        if (adr == null) return false; // not found

        // Property Read
        if (bacnet_client.ReadPropertyRequest(adr, BacnetObjet, Propriete, out NoScalarValue) == false)
            return false;

        Value = NoScalarValue[0];
        return true;
    }

    static BacnetAddress DeviceAddr(uint device_id)
    {
        BacnetAddress ret;

        lock (DevicesList)
        {
            foreach (BacNode bn in DevicesList)
            {
                ret = bn.getAdd(device_id);
                if (ret != null) return ret;
            }

            // not in the list
            return null;
        }
    }
}