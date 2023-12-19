using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class UMPConsentUtils
{
    // 判断所在地区是否受到GDPR政策的约束
    public static bool IsGDPR()
    {
        int gdpr = PlayerPrefs.GetInt("IABTCF_gdprApplies", 0);
        return gdpr == 1;
    }

    // 判断是否可以展示广告
    public static bool CanShowAds()
    {
        string purposeConsent = PlayerPrefs.GetString("IABTCF_PurposeConsents", "");
        string vendorConsent = PlayerPrefs.GetString("IABTCF_VendorConsents", "");
        string vendorLI = PlayerPrefs.GetString("IABTCF_VendorLegitimateInterests", "");
        string purposeLI = PlayerPrefs.GetString("IABTCF_PurposeLegitimateInterests", "");

        int googleId = 755;
        bool hasGoogleVendorConsent = HasAttribute(vendorConsent, googleId);
        bool hasGoogleVendorLI = HasAttribute(vendorLI, googleId);

        // 至少对于非个性化广告，需要满足的最低要求
        return HasConsentFor(new List<int> {1}, purposeConsent, hasGoogleVendorConsent) &&
               HasConsentOrLegitimateInterestFor(new List<int> {2, 7, 9, 10}, purposeConsent, purposeLI, hasGoogleVendorConsent, hasGoogleVendorLI);
    }

    // 判断是否可以展示个性化广告
    public static bool CanShowPersonalizedAds()
    {
        string purposeConsent = PlayerPrefs.GetString("IABTCF_PurposeConsents", "");
        string vendorConsent = PlayerPrefs.GetString("IABTCF_VendorConsents", "");
        string vendorLI = PlayerPrefs.GetString("IABTCF_VendorLegitimateInterests", "");
        string purposeLI = PlayerPrefs.GetString("IABTCF_PurposeLegitimateInterests", "");

        int googleId = 755;
        bool hasGoogleVendorConsent = HasAttribute(vendorConsent, googleId);
        bool hasGoogleVendorLI = HasAttribute(vendorLI, googleId);

        return HasConsentFor(new List<int> {1, 3, 4}, purposeConsent, hasGoogleVendorConsent) &&
               HasConsentOrLegitimateInterestFor(new List<int> {2, 7, 9, 10}, purposeConsent, purposeLI, hasGoogleVendorConsent, hasGoogleVendorLI);
    }
    public static bool  CanShowPersonalizedAds2()
{
    string purposeConsent = PlayerPrefs.GetString("IABTCF_PurposeConsents", "");
    Debug.Log("purposeConsent : " + purposeConsent); //11111111111
    string vendorConsent = PlayerPrefs.GetString("IABTCF_VendorConsents", "");
    Debug.Log("vendorConsent : " + vendorConsent); //100100001111101100001111101100010101101101001000010100001011000000011110100111001101101000101110110000000000110000000010000100011101000110110000001000000001000010100001000000000000000000000001001000000100000010011000000000000101000000000000101001000000100000000001000000001010000010010000000001000000010100000001001010000000000100000000000000001000000000000000000000000000100000001001100100000100100001000000000000010000001000000000000000000000000001010000000000000001000000000000000000000000000000000000010000010000000000000000000000000000000000000000000000100000100000000000000000000010000000000000010000000000000000000000000000100000000000000000000000001000000000100000000000000000010000000000000000100000000000000000000000000000010000000000000000000010011000000010000000000000000000100000100000000000001000000000000000000010000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001

    string vendorLI = PlayerPrefs.GetString("IABTCF_VendorLegitimateInterests", "");
    Debug.Log("vendorLI : " + vendorLI); //100000000111101100001011101100010101101101001000010100001011001000011110000101001100100000101100110000000000100000000010000101000101000110110000001000000001000010100000000000000000000000000001001000000100000010010000000000000001000000000000101001000000100000000001000000001010010000010000000001000000000000010000001010000000000000000000000000000000000000000000000000000000100000001001000000000100100000000000000000100000000000000010000000000000000000010000000000000101000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000100000100000000000000000000000000000000000010000000000000000000000000000000000000000000000000000001000000000100000000000000000000000000000000000100000000000000000000000000000010000000000000000000010001001000010000100000000000000100000100000000000001000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000000000000000010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001

    string purposeLI = PlayerPrefs.GetString("IABTCF_PurposeLegitimateInterests", "");
      Debug.Log("purposeLI : " + purposeLI);//01000011111

    int googleId = 755;
    bool hasGoogleVendorConsent = HasAttribute(vendorConsent, googleId);
    Debug.Log("hasGoogleVendorConsent : " + hasGoogleVendorConsent); //False
    bool hasGoogleVendorLI = HasAttribute(vendorLI, googleId);

    Debug.Log("hasGoogleVendorLI : " + hasGoogleVendorLI);//False
    // 检查是否有第1, 3, 4目的的同意，并且供应商同意也存在
    bool hasRequiredPurposeConsent = HasConsentFor(new List<int> {1, 3, 4}, purposeConsent, hasGoogleVendorConsent);
        Debug.Log("hasRequiredPurposeConsent : " + hasRequiredPurposeConsent); //false
    // 检查是否对第2, 7, 9, 10目的有同意或合法利益，并且供应商同意或合法利益也存在
    bool hasRequiredPurposeConsentOrLI = HasConsentOrLegitimateInterestFor(new List<int> {2, 7, 9, 10}, purposeConsent, purposeLI, hasGoogleVendorConsent, hasGoogleVendorLI);
    Debug.Log("hasRequiredPurposeConsentOrLI : " + hasRequiredPurposeConsentOrLI); //false
    return hasRequiredPurposeConsent && hasRequiredPurposeConsentOrLI;
}

    private static bool HasAttribute(string input, int index)
    {
        return !string.IsNullOrEmpty(input) && input.Length >= index && input[index - 1] == '1';
    }

    private static bool HasConsentFor(List<int> purposes, string purposeConsent, bool hasVendorConsent)
    {
        return purposes.All(p => HasAttribute(purposeConsent, p)) && hasVendorConsent;
    }

    private  static bool HasConsentOrLegitimateInterestFor(List<int> purposes, string purposeConsent, string purposeLI, bool hasVendorConsent, bool hasVendorLI)
    {
        return purposes.All(p => (HasAttribute(purposeConsent, p) && hasVendorConsent) ||
                                 (HasAttribute(purposeLI, p) && hasVendorLI));
    }
}
