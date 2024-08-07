namespace SunamoShared._public.SunamoInterfaces.Interfaces;


public interface IPercentCalculator
{
    double _overallSum { get; set; }
    double last { get; set; }
    IPercentCalculator Create(double overallSum);
    void AddOnePercent();
    int PercentFor(double value, bool last);
    void ResetComputedSum();
}