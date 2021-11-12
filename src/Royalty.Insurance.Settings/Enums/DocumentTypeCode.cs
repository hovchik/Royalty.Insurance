namespace Royalty.Insurance.Settings.Enums
{
    public enum DocumentTypeCode : byte
    {
        RoyaltyForms = 1,
        Supplement = 2,
        AccordForms = 3,
        /// <summary>
        /// Generated document which is in progress
        /// </summary>
        GeneratedDocuments = 4,
        /// <summary>
        /// Upload document which is not assigned to any insured- others
        /// </summary>
        SharepointShared = 5,

        /// <summary>
        /// Generated and sign document
        /// </summary>
        StorageUploaded = 6
    }
}
