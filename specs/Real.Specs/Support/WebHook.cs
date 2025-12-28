using Real.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Reqnroll.BoDi;
using Reqnroll;
using Reqnroll.Assist.ValueRetrievers;
using Reqnroll.Assist;

namespace Real.Support;

[Binding]
public class WebHook //: IClassFixture<RealWebFactory<Program>>
{
    private readonly IObjectContainer _objectContainer;

    private readonly IReqnrollOutputHelper _reqnollOutputHelper;

    private readonly WebApplicationFactory<Program> _webFactory;

    private readonly HttpClient _httpClient;

    private readonly RealDbContext _db;

    private readonly IServiceScope _serviceScope;

    private int _counter = 0;

    public WebHook(
        IObjectContainer objectContainer,
        IReqnrollOutputHelper reqnollOutputHelper,
        RealWebFactory<Program> webFactory)
    {
        _objectContainer = objectContainer;

        _reqnollOutputHelper = reqnollOutputHelper;

        _counter++;

        reqnollOutputHelper.WriteLine($"WebHook --> {_counter}");

        _webFactory = webFactory;

        _httpClient = _webFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });

        //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(scheme: "TestScheme");

        objectContainer.RegisterInstanceAs(_httpClient);

        //

        _serviceScope = webFactory.Services.CreateScope();

        var scopedServices = _serviceScope.ServiceProvider;

        _db = scopedServices.GetRequiredService<RealDbContext>();

        objectContainer.RegisterInstanceAs(_db);

        var dateTimeSnapshot = scopedServices.GetRequiredService<DateTimeSnapshot>();

        objectContainer.RegisterInstanceAs(dateTimeSnapshot);

        reqnollOutputHelper.WriteLine($"RegisterInstanceAs --> dateTimeSnapshot");
    }

    [BeforeScenario(Order = 0)]
    public void InitializeWeb(FeatureContext feature, ScenarioContext scenario)
    {
        _reqnollOutputHelper.WriteLine("EnsureDeleted");

        _db.Database.EnsureDeleted();

        _reqnollOutputHelper.WriteLine("Migrate");

        _db.Database.Migrate();

        //

        //objectContainer.RegisterInstanceAs<CalculoDeTaxaDeMarcacao>(calculoDeTaxaDeMarcacaoWeb);

        //if (feature.FeatureInfo.Title == "Cálculo (ou Calculadora) de Taxa de Marcação")
        //{
        //    var calculoDeTaxaDeMarcacaoWeb = new CalculoDeTaxaDeMarcacaoWeb(httpClient);

        //    objectContainer.RegisterInstanceAs<CalculoDeTaxaDeMarcacao>(calculoDeTaxaDeMarcacaoWeb);
        //}
        //else if (feature.FeatureInfo.Title == "Cadastro de Modelos")
        //{
        //    var cadastroDeModelosWeb = new CadastroDeModelosWeb(httpClient);

        //    objectContainer.RegisterInstanceAs<CadastroDeModelos>(cadastroDeModelosWeb);
        //}
    }

    [AfterScenario]
    public void Dispose()
    {
        _serviceScope.Dispose();

        _db.Dispose();
    }

    //[BeforeTestRun]
    //public static void BeforeTestRunInjection(ITestRunnerManager testRunnerManager, ITestRunner testRunner)
    //{
    //    //All parameters are resolved from the test thread container automatically.
    //    //Since the global container is the base container of the test thread container, globally registered services can be also injected.

    //    //ITestRunManager from global container
    //    var location = testRunnerManager.TestAssembly.Location;

    //    //ITestRunner from test thread container
    //    //var threadId = testRunner.ThreadId;

    //    Service.Instance.ValueRetrievers.Register(new NullValueRetriever("<null>"));
    //    Service.Instance.ValueComparers.Register(new NullValueComparer("<null>"));
    //}
}
