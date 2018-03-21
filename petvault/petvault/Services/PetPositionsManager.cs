using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using petvault.Models;

namespace petvault.Helpers
{
    public class PetPositionsManager
    {
        public static MobileServiceClient MobileService = new MobileServiceClient("https://petvault.azurewebsites.net");

        public static async Task<ObservableCollection<PetPositions>> GetPetPositionsItemsAsync(bool syncItems = false)
        {
            try
            {
#if OFFLINE_SYNC_ENABLED
                if(syncItems)
                {
                    await this.SyncAsync();
                }
#endif
                IEnumerable<PetPositions> items = await MobileService.GetTable<PetPositions>()
                                                                 .ToEnumerableAsync();

                return new ObservableCollection<PetPositions>(items);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
