using Cities_of_Mosaic_Isle_PublicInterfaces.InGame;

namespace Cities_of_Mosaic_Isle_PublicInterfaces.Helper
{
    public interface IFModdableGameConsts
    {
        //this class holds numerical values (Int64, double) which represent a constant in calculations done by the game or its scripts.
        //the major difference between this interface (and classes which derive from it) and IFModdableCustomConsts is that all consts here must exist.
        //  a minor difference is that each const has its individual function, so an incorrectly named const will cause a compile error.
        //  (IFModdableCustomConsts will not error when asked for the value of a const that does not exist; it will simply return certain values.)

        //GUARANTEE SECTION:
        //here we list the guarantees that a derived class must meet as a function of its existence as an IFModdableGameConsts:
        //A) This class represents a moddable object; therefore, by the time any script can access members of this class, the return values of all functions will only depend on the function's inputs.

        //average input factor is calculated based on other game consts, not set.  Put it at the top of the interface here:
        public double getAverageInputFactor();
        //many places want to print out resources to parts of the UI with +, -, green color, red color, and the quantity and quality of the resource.  These functions help to do so. //TODO use these more in C# coded displays and scripts
        public string getQuantityFormattedString(double inQuantity);
        public string getQualityFormattedString(double inQuality);
        public string getResourceFormattedString(IFResource inResource, double inQuantity, double inQuality);


        //the remaining functions are otherwise all accessors to game consts:
        public Int64 getResourcePoolMax();
        public double getResourceQualityMax();
        public double getResourceQualityMin();
        public double getResourceQualityMaxChoosable();
        public double getResourceQualityMinChoosable();

        public double getHealthMax();
        public double getHealthMin();
        public double getHealthLowThreshold();
        public double getHealthHighThreshold();

        public double getSoldierSkillMax() ;
        public double getSoldierSkillMin() ;
        public double getWorkerSkillMin() ;
        public double getWorkerSkillMax();

        public double getProductionNoResourceInputQuality() ;
        public double getProductionMinResourceInputQuality() ;
        public double getProductionAverageResourceInputQuality() ;

        public double getProductionWorkerSkillFactorMin() ;
        public double getProductionWorkerSkillFactorMax() ;

        public double getProductionXCenterPPF() ;
        public double getProductionYCenterPPF() ;

        public double getProductionElasticityOfSubForComplements() ;
        public double getProductionElasticityOfSubForSubstitutes() ;
        public double getProductionProportionBonusForComplements() ;
        public double getProductionProportionBonusForSubstitutes() ;

        public Int64 getMapMinWidth() ;
        public Int64 getMapMaxWidth() ;
        public Int64 getMapTargetArea() ;

        public Int64 getMapBaseParcelMaxWidth() ;
        public Int64 getMapDoodadMaxWidth() ;
        public Int64 getMapResourceParcelMaxWidth() ;
        public Int64 getSandboxPopCount() ; //TODO remove this
        public Int64 getMapGenAttemptsBeforeHalt() ;

        public double getDesolationMaxTileValue() ;

        public double getPopVelocity() ;
        public double getWorldMapPixelsToMove() ;
        public double getLocalMapMaxViewZoom() ;
        public double getLocalMapMinViewZoom() ;
        public double getLocalMapPixelsToMove() ;
        public double getLocalMapZoomFactor() ;
        public double getDiploMapMinViewZoom();
        public double getDiploMapMaxViewZoom();
        public double getDiploMapPixelsToMove();
        public double getDiploMapZoomFactor();
        public double getDelegationVelocityBase();
        public double getEconStrengthMax();
        public double getMilStrengthMax();
        public double getHappinessMin();
        public double getHappinessMax();

        public double getPopVelocityIllnessSlowFactor();
        public double getPopVelocityOverallHealthSlowFactor();

        public double getMoraleMax();

        public double getHappinessLowAlertThreshold();
        public Int64 getCalendarYearOffset();
        public Int64 getCalendarPopAgeOffset();
        public double getClusteringRadiusPopsPlacedOnMap();

        //display thresholds: LM means low/medium threshold.  MH means medium/high threshold
        //ratio thresholds are all between 0.0d and 1.0d inclusive
        public double getDisplayThreshold_PopGearRatioLM();
        public double getDisplayThreshold_PopGearRatioMH();
        public double getDisplayThreshold_BuildingDurabilityRatioLM(); //note that this is "durability missing is low-medium"
        public double getDisplayThreshold_BuildingDurabilityRatioMH(); //note that this is "durability missing is medium-high"
        public double getDisplayThreshold_BuildingFillRatioLM();
        public double getDisplayThreshold_BuildingFillRatioMH();
        public double getDisplayThreshold_ResourceParcelDesolationRatioLM();
        public double getDisplayThreshold_ResourceParcelDesolationRatioMH();

        //absolute value thresholds are direct counts.  They are always positive but might be 0
        public Int64 getDisplayThreshold_DelegationPopCountLM();
        public Int64 getDisplayThreshold_DelegationPopCountMH();
        public Int64 getDisplayThreshold_CommunityPopCountLM();
        public Int64 getDisplayThreshold_CommunityPopCountMH();

        public Int64 getCalendarSeasonsPerYear();
        public Int64 getCalendarMonthsPerSeason();
        public Int64 getCalendarDaysPerMonth();
        public Int64 getCalendarDaysPerSeason();
        public Int64 getCalendarMonthsPerYear();
        public Int64 getCalendarDaysPerYear();

        public Int64 getPopKeepAfterDeadDaysGeneral();
        public Int64 getDelegationMaximumTravelTime();
        public bool getShouldCustomScriptExceptionsBeLogged();
        public Int64 getCustomScriptExceptionErrorReportLevel();
        public Int64 getMaxCountOfAStarObjects();
        public double getSpeedIncreaseOfRoadBridgeCanal();
        public double getBoxSelectorMinimumRadius();
        public double getActionMenuSoldierSkillFilterProportionToMove();
        public double getActionMenuSoldierAvgEquipmentQualityProportionToMove();

        public Int64 getCountOfDivisionsOfHistoryCostBenefit();
        public Int64 getCountOfDaysToRememberDailyHistory();
        public Int64 getCountOfMonthsToRememberMonthlyHistory();
        public Int64 getCountOfSeasonsToRememberSeasonalHistory();
        public Int64 getCountOfYearsToRememberYearlyHistory();
    }
}
