using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CalloutAPI;

namespace WelfareCheck
{
    [CalloutProperties("Welfare check", "FivePD", "1.0", Probability.Medium)]
    public class WelfareCheck : CalloutAPI.Callout
    {
        Ped person;

        public WelfareCheck()
        {
            Random rnd = new Random();
            Vector3[] coordinates =
            {
                // paleto bay
                new Vector3(-367.5025f,6213.217f,31.84226f),
                new Vector3(-454.3175f,6201.962f,29.55285f),
                new Vector3(-106.53f,6334.514f,35.50075f),
                new Vector3(438.0112f,6504.859f,28.70074f),

                // grapeseed
                new Vector3(2397.687f,5034.988f,45.99347f),
                new Vector3(1964.67f,5178.501f,47.90499f),
                new Vector3(1659.086f,4756.857f,41.99194f),
                new Vector3(1724.002f,4642.021f,43.87545f),

                // sandy shores
                new Vector3(1899.968f,3771.797f,32.88044f),
                new Vector3(1889.489f,3891.265f,32.81117f),
                new Vector3(1661.85f,3820.346f,35.46971f),
                new Vector3(1404.187f,3657.898f,34.10057f),
                new Vector3(1287.079f,3628.812f,32.72287f),
                new Vector3(993.8249f,3579.059f,33.70324f),
                new Vector3(388.4751f,3585.679f,33.29226f),

                // los santos
                new Vector3(83.20876f,485.3784f,148.1926f),
                new Vector3(-751.8965f,623.7001f,142.4337f),
                new Vector3(-1289.77f,615.9083f,139.258f),
                new Vector3(-1625.072f,37.14625f,62.54136f),
                new Vector3(-598.2375f,148.2733f,61.67282f),
                new Vector3(-159.5558f,126.3908f,70.22538f),
                new Vector3(234.5608f,-105.517f,74.35268f),
                new Vector3(96.8438f,-253.5587f,47.41634f),
                new Vector3(-1178.988f,-371.8854f,36.62564f),
                new Vector3(-1283.123f,-1252.036f,4.078192f),
                new Vector3(-1108.771f,-1528.094f,6.77574f),
                new Vector3(-890.6318f,-1517.141f,5.177319f),
                new Vector3(-903.0381f,-1286.018f,5.20743f),
                new Vector3(495.1448f,-1821.431f,28.86942f),
                new Vector3(248.6486f,-1934.828f,24.31248f),
            };

            Vector3 spawnLocation;
            do
            {
                spawnLocation = coordinates[rnd.Next(0, coordinates.Length)];
            } while (Game.PlayerPed.IsInRangeOf(spawnLocation, 600f) && !Game.PlayerPed.IsInRangeOf(spawnLocation, 150f));
            InitBase(spawnLocation);

            ShortName = "Welfare check";
            CalloutDescription = "Caller reported something";
            ResponseCode = 3;
            StartDistance = 150f;
        }

        public async override Task Init()
        {
            OnAccept();

            person = await SpawnPed(GetRandomPed(), Location);

            person.AlwaysKeepTask = true;
            person.BlockPermanentEvents = true;
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);

            person.Task.StandStill(-1);

            Random rnd = new Random();
            int x = rnd.Next(0, 3);
            if (x == 0) person.Kill();
        }
    }
}
