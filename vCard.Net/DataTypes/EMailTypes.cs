using System;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This enumerated type defines the various e-mail types for the <see cref="Email"/>
    /// </summary>
    [Serializable]
    [Flags]
    public enum EMailTypes
    {
        /// <summary>
        /// Indicates no type specified
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Indicates preferred e-mail (PREF) address (vCard 2.1 and 3.0 only)
        /// </summary>
        Preferred = 0x1,
        /// <summary>
        /// Indicates America On-Line (AOL)
        /// </summary>
        AOL = 0x2,
        /// <summary>
        /// Indicates AppleLink (AppleLink)
        /// </summary>
        AppleLink = 0x4,
        /// <summary>
        /// Indicates AT&#x26;T Mail (ATTMail)
        /// </summary>
        ATTMail = 0x8,
        /// <summary>
        /// Indicates CompuServe Information Service (CIS)
        /// </summary>
        CompuServe = 0x10,
        /// <summary>
        /// Indicates eWorld (eWorld)
        /// </summary>
        eWorld = 0x20,
        /// <summary>
        /// Indicates Internet SMTP (INTERNET, the default)
        /// </summary>
        Internet = 0x40,
        /// <summary>
        /// Indicates IBM Mail (IBMMail)
        /// </summary>
        IBMMail = 0x80,
        /// <summary>
        /// Indicates MCI Mail (MCIMail)
        /// </summary>
        MCIMail = 0x100,
        /// <summary>
        /// Indicates PowerShare (POWERSHARE)
        /// </summary>
        PowerShare = 0x200,
        /// <summary>
        /// Indicates Prodigy information service (PRODIGY)
        /// </summary>
        Prodigy = 0x400,
        /// <summary>
        /// Indicates Telex number (TLX)
        /// </summary>
        Telex = 0x800,
        /// <summary>
        /// Indicates X.400 service (X400)
        /// </summary>
        X400 = 0x1000
    }
}