using Real.Data;
using Real.Models;

namespace Real.StepDefinitions;

[Binding]
public class CriacaoCategoriasStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;

    private readonly FeatureContext _featureContext;

    private readonly RealDbContext _db;

    public CriacaoCategoriasStepDefinitions(
        RealDbContext db)
    {
        _db = db;
    }

}

