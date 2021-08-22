using System;
using System.Collections.Generic;
using System.Text;

namespace MarcTron.Plugin
{
    public enum MTTagForChildDirectedTreatment
    {
        TagForChildDirectedTreatmentUnspecified = -1,
        TagForChildDirectedTreatmentFalse,
        TagForChildDirectedTreatmentTrue
    }

    public enum MTTagForUnderAgeOfConsent
    {
        TagForUnderAgeOfConsentUnspecified = -1,
        TagForUnderAgeOfConsentFalse,
        TagForUnderAgeOfConsentTrue,
    }

    public enum MTMaxAdContentRating
    {
        MaxAdContentRatingG,
        MaxAdContentRatingPg,
        MaxAdContentRatingT,
        MaxAdContentRatingMa,
        MaxAdContentRatingUnspecified
    }

    //public static class MTMaxAdContentRating
    //{
    //    public static string MaxAdContentRatingG = "G";
    //    public static string MaxAdContentRatingT = "T";
    //    public static string MaxAdContentRatingMa = "MA";
    //    public static string MaxAdContentRatingPg = "PG";
    //}
}
