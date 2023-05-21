using System;
using System.Collections.Generic;
using System.Text;

namespace MarcTron.Plugin.Extra
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
}
