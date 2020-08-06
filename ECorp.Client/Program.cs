using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;
using ECorp.UI;
using ECorp.UI.Services;
using Csla.Configuration;
using TG.Blazor.IndexedDB;
using ECorp.DataAccess.Contact;
using ECorp.DataAccess.Wasm;
using ECorp.DataAccess.Remote;
using Csla;
using ECorp.DataAccess;

namespace ECorp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            System.Diagnostics.Debug.WriteLine("In Main fucntion of ECorp.Client program.cs");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddTransient(typeof(Csla.Blazor.ViewModel<>), typeof(Csla.Blazor.ViewModel<>));
            builder.Services.AddScoped<CWBApiAuthorizationHandler>();
            builder.Services.AddTelerikBlazor();
            //Now create a named service instance for HttpClient that points to the correct base address (based on your handler configuration) and instantiates the right authorization handler for that client
            //See FetchData.razor to see how we then use this client
            builder.Services.AddHttpClient("ServerAPI",
                client => client.BaseAddress = new Uri("https://localhost:44352/api"))
                .AddHttpMessageHandler<CWBApiAuthorizationHandler>(); ;
            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://nsdevelopment.onmicrosoft.com/DualBlazor/user_impersonation");
            });

            #region IndexedDb Setup
            builder.Services.AddIndexedDB(dbStore =>
            {
                dbStore.DbName = "D365Entities";
                //Important!  Every time you decide to add another table like you're doing in the the dbStore.Stores.Add call below, increment the dbStore.Version.  That way you can extend the database schema without losing the already persisted data
                //If you mess up your PrimaryKey definition(s) you may need to fiddle around to get rid of the messed up table -> changing the Version won't fix the problem (it seems).  Instead you need to either delete the table or delete the database and start again
                //You can manage IndexedDb instances in the browser -> Go to Developer Tools/Application
                dbStore.Version = 3;
                dbStore.Stores.Add(new StoreSchema
                {
                    Name = "Accounts",
                    //The Name and KeyPath are json cased, meaning the first character of the field name is lower case regardless of how you named the Property in your class
                    //For instance, FirstName becomes firstName, and Id becomes id
                    //If you don't get it right when you try to insert records the insert will fail
                    PrimaryKey = new IndexSpec
                    {
                        Name = "accountid",
                        KeyPath = "accountid",
                        Auto = false,
                        Unique = true
                    },
                    Indexes = new List<IndexSpec>
                    {
                        new IndexSpec{Name="name", KeyPath="name", Auto=false}
                    }
                });
                dbStore.Stores.Add(new StoreSchema
                {
                    Name = "Forecasts",
                    PrimaryKey = new IndexSpec
                    {
                        Name = "id",
                        KeyPath = "id",
                        Auto = false
                    }
                });
                dbStore.Stores.Add(new StoreSchema
                {
                    Name = "Contacts",
                    PrimaryKey = new IndexSpec
                    {
                        Name = "id",
                        KeyPath = "id",
                        Auto = false,
                        Unique = true
                    }
                });
            }
);
            #endregion

            #region CSLA Data Services Setup
            builder.UseCsla((c) =>
                            c.DataPortal());
            builder.Services.AddScoped<IContactDal, ContactDal>();
            builder.Services.AddScoped<IRemoteContactDal, RemoteContactDal>();
            #endregion
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ValuesService>();
            builder.Services.AddScoped<IBackgroundDataRefreshService<ContactEntity>, ContactsBackgroundRefreshService<ContactEntity>>();
            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
