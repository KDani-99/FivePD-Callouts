using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using CalloutAPI;

namespace CyclistOnHighway
{
    [CalloutProperties("Cyclist on highway", "FivePD", "1.0", Probability.Medium)]
    public class CyclistOnHighway : CalloutAPI.Callout
    {
        Ped suspect;
        Vehicle bike;
        Vector3[] coordinates = {
            new Vector3(1308.065f,581.2718f,79.78977f),
            new Vector3(1594.56f,1009.64f,78.95673f),
            new Vector3(1689.932f,1352.383f,87.02444f),
            new Vector3(1878.03f,2336.891f,55.68655f),
            new Vector3(2061.051f,2644.353f,52.16157f),
            new Vector3(2360.496f,2856.932f,40.1833f),
            new Vector3(2539.596f,3042.605f,42.92778f),
            new Vector3(2946.281f,3813.16f,52.26584f),
            new Vector3(2795.884f,446.548f,48.0796f),
            new Vector3(2649.814f,4928.652f,44.39455f),
            new Vector3(2331.14f,5905.173f,47.67876f),
            new Vector3(1436.997f,6474.696f,20.40421f),
            new Vector3(777.1246f,6513.02f,24.64042f),
            new Vector3(-589.123f,5663.769f,38.00635f),
            new Vector3(-1529.918f,4981.798f,62.087722f),
            new Vector3(-2329.675f,4112.701f,35.33438f),
            new Vector3(-589.123f,5663.769f,38.00635f),
            new Vector3(-1529.918f,4981.798f,62.08722f),
            new Vector3(-2329.675f,4112.701f,35.33438f),
            new Vector3(-2620.146f,2824.454f,16.38638f),
            new Vector3(-3039.727f,1872.351f,29.84845f),
            new Vector3(-3128.839f,835.1783f,16.17631f),
            new Vector3(-2539.692f,-185.8579f,19.42014f),
            new Vector3(-1842.228f,-595.9995f,11.09579f),
        };

        public CyclistOnHighway()
        {
            Random rnd = new Random();

            Vector3 spawnLocation;
            do
            {
                spawnLocation = coordinates[rnd.Next(0, coordinates.Length)];
            } while (Game.PlayerPed.IsInRangeOf(spawnLocation, 1100f) && !Game.PlayerPed.IsInRangeOf(spawnLocation, 200f));

            InitBase(spawnLocation);

            ShortName = "Cyclist on highway";
            CalloutDescription = "We've got a call that somebody is cycling on the freeway.";
            ResponseCode = 2;
            StartDistance = 200f;
        }

        public async override Task Init()
        {
            OnAccept();

            suspect = await SpawnPed(GetRandomPed(), Location);
            bike = await SpawnVehicle(VehicleHash.TriBike, Location);

            suspect.AlwaysKeepTask = true;
            suspect.BlockPermanentEvents = true;
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);

            suspect.Task.WarpIntoVehicle(bike, VehicleSeat.Driver);
            suspect.Task.CruiseWithVehicle(bike, 60f);
        }
    }
}
