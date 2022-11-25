using System;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This enumerated type defines the various phone types for the <see cref="Telephone"/>
    /// </summary>
    [Serializable]
    [Flags]
    public enum PhoneTypes
    {
        /// <summary>
        /// Indicates no type specified
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Indicates preferred (PREF) number (vCard 2.1 and 3.0 only)
        /// </summary>
        Preferred = 0x1,
        /// <summary>
        /// Indicates a work (WORK) number
        /// </summary>
        Work = 0x2,
        /// <summary>
        /// Indicates a home (HOME) number
        /// </summary>
        Home = 0x4,
        /// <summary>
        /// Indicates a voice (VOICE) number (the default)
        /// </summary>
        Voice = 0x8,
        /// <summary>
        /// Indicates a facsimile (FAX) number
        /// </summary>
        Fax = 0x10,
        /// <summary>
        /// Indicates a messaging service (MSG) on the number
        /// </summary>
        Message = 0x20,
        /// <summary>
        /// Indicates a cellular (CELL) number
        /// </summary>
        Cell = 0x40,
        /// <summary>
        /// Indicates a pager (PAGER) number
        /// </summary>
        Pager = 0x80,
        /// <summary>
        /// Indicates a bulletin board service (BBS) number
        /// </summary>
        BBS = 0x100,
        /// <summary>
        /// Indicates a modem (MODEM) number
        /// </summary>
        Modem = 0x200,
        /// <summary>
        /// Indicates a car-phone (CAR) number
        /// </summary>
        Car = 0x400,
        /// <summary>
        /// Indicates an ISDN (ISDN) number
        /// </summary>
        ISDN = 0x800,
        /// <summary>
        /// Indicates a video-phone (VIDEO) number
        /// </summary>
        Video = 0x1000,
        /// <summary>
        /// Indicates a personal communication services (PCS) telephone number (3.0 specification only)
        /// </summary>
        PCS = 0x2000,
        /// <summary>
        /// Indicates a text message (SMS) number (4.0 specification only)
        /// </summary>
        Text = 0x4000,
        /// <summary>
        /// Indicates a number for a telecommunication device for people with hearing or speech difficulties (4.0 specification only)
        /// </summary>
        TextPhone = 0x8000
    }
}