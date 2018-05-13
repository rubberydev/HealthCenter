
namespace HealthCenter.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        public static string Date
        {
            get { return Resource.Accept; }
        }
        
        public static string AccessInvalid
        {
            get { return Resource.AccessInvalid; }
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }
        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string EmailValidation
        {
            get { return Resource.EmailValidator; }
        }

        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }

        public static string Password
        {
            get { return Resource.Password; }
        }

        public static string Email
        {
            get { return Resource.Email; }
        }

        public static string Login
        {
            get { return Resource.Login; }
        }

        public static string Register
        {
            get { return Resource.Register; }
        }

        public static string BibleTitle
        {

            get { return Resource.BibleTitle; }
        }

        public static string BibliesTitle
        {

            get { return Resource.BibliesTitle; }
        }

        public static string BookTitle
        {

            get { return Resource.BookTitle; }
        }

        public static string EmailPlaceHolder
        {

            get { return Resource.EmailPlaceHolder; }
        }

        public static string LoginTitle
        {

            get { return Resource.LoginTitle; }
        }

        public static string PasswordPlaceHolder
        {

            get { return Resource.PasswordPlaceHolder; }
        }

        public static string PasswordValidator
        {

            get { return Resource.PasswordValidator; }
        }

        public static string ErrorConnection
        {

            get { return Resource.ErrorConnection; }
        }

        public static string ErrorToken
        {

            get { return Resource.ErrorToken; }
        }

        public static string ErrorPassword
        {

            get { return Resource.ErrorPassword; }
        }

        public static string MyProfile
        {

            get { return Resource.MyProfile; }
        }

        public static string LogOut
        {

            get { return Resource.LogOut; }
        }

        public static string Menu
        {

            get { return Resource.Menu; }
        }

        public static string lastValidation
        {

            get { return Resource.lastValidation; }
        }

        public static string Registration
        {

            get { return Resource.Registration; }
        }

        public static string FirstNameValidation
        {

            get { return Resource.FirstNameValidation; }
        }

        public static string LastNameValidation
        {

            get { return Resource.LastNameValidation; }
        }

        public static string EmailValidation2
        {

            get { return Resource.EmailValidation2; }
        }

        public static string PhoneValidation
        {

            get { return Resource.PhoneValidation; }
        }

        public static string PasswordValidation
        {

            get { return Resource.PasswordValidation; }
        }

        public static string PasswordValidation2
        {

            get { return Resource.PasswordValidation; }
        }

        public static string ConfirmValidation
        {

            get { return Resource.ConfirmValidation; }
        }

        public static string ConfirmValidation2
        {

            get { return Resource.ConfirmValidation2; }
        }

        public static string ConfirmLabel
        {

            get { return Resource.ConfirmLabel; }
        }

        public static string UserRegisteredMessage
        {

            get { return Resource.UserRegisteredMessage; }
        }

        public static string Cancel
        {

            get { return Resource.Cancel; }
        }

        public static string SourceImageQuestion
        {

            get { return Resource.SourceImageQuestion; }
        }

        public static string FromGallery
        {

            get { return Resource.FromGallery; }
        }

        public static string FromCamera
        {

            get { return Resource.FromCamera; }
        }

        public static string ChangeImage
        {

            get { return Resource.ChangeImage; }
        }

        public static string FirstNameLabel
        {

            get { return Resource.FirstNameLabel; }
        }

        public static string FirstNamePlaceHolder
        {

            get { return Resource.FirstNamePlaceHolder; }
        }

        public static string LastNameLabel
        {

            get { return Resource.LastNameLabel; }
        }

        public static string LastNamePlaceHolder
        {

            get { return Resource.LastNamePlaceHolder; }
        }

        public static string PhoneLabel
        {

            get { return Resource.PhoneLabel; }
        }

        public static string PhonePlaceHolder
        {

            get { return Resource.PhonePlaceHolder; }
        }

        public static string ConfirmPassword
        {

            get { return Resource.ConfirmPassword; }
        }

        public static string ConfirmPlaceHolder
        {

            get { return Resource.ConfirmPlaceHolder; }
        }

        public static string Search
        {

            get { return Resource.Search; }
        }

        public static string nextValidation
        {

            get { return Resource.nextValidation; }
        }

        public static string SearchPage
        {

            get { return Resource.SearchPage; }
        }

        public static string CurrentPassword
        {

            get { return Resource.CurrentPassword; }
        }


        public static string CurrentPasswordPlaceHolder
        {

            get { return Resource.CurrentPasswordPlaceHolder; }
        }

        public static string NewPassword
        {

            get { return Resource.NewPassword; }
        }


        public static string NewPasswordPlaceHolder
        {

            get { return Resource.NewPasswordPlaceHolder; }
        }

        public static string ConfirmPassPlaceHolder
        {

            get { return Resource.ConfirmPassPlaceHolder; }
        }

        public static string ChangePassword
        {

            get { return Resource.ChangePassword; }
        }

        public static string PasswordError
        {

            get { return Resource.PasswordError; }
        }

        public static string ErrorChangingPassword
        {

            get { return Resource.ErrorChangingPassword; }
        }

        public static string ChagePasswordConfirm
        {

            get { return Resource.ChagePasswordConfirm; }
        }

        public static string ConnectionError1
        {

            get { return Resource.ConnectionError1; }
        }

        public static string ConnectionError2
        {

            get { return Resource.ConnectionError2; }
        }

        public static string Update
        {

            get { return Resource.Update; }
        }

        public static string SearchBible
        {

            get { return Resource.SearchBible; }
        }

        public static string SelectBible
        {

            get { return Resource.SelectBible; }
        }

        public static string SelectBook
        {

            get { return Resource.SelectBook; }
        }

        public static string DigitChapter
        {

            get { return Resource.DigitChapter; }
        }

        public static string PlaceHolderChapter
        {

            get { return Resource.PlaceHolderChapter; }
        }

        public static string DigitVerses
        {

            get { return Resource.DigitVerses; }
        }

        public static string PlaceHolderVerses
        {

            get { return Resource.PlaceHolderVerses; }
        }

        public static string SearchVerses
        {

            get { return Resource.SearchVerses; }
        }

        public static string SelectBibleValidator
        {

            get { return Resource.SelectBibleValidator; }
        }

        public static string SearchBibleValidator
        {

            get { return Resource.SearchBibleValidator; }
        }

        public static string ChapterValidator
        {

            get { return Resource.ChapterValidator; }
        }

        public static string ResultSearch
        {

            get { return Resource.ResultSearch; }
        }

        public static string ChapterValidator2
        {

            get { return Resource.ChapterValidator2; }
        }

        public static string ChapterValidatorNumber
        {

            get { return Resource.ChapterValidatorNumber; }
        }

        public static string VerseValidatorNumber
        {

            get { return Resource.VerseValidatorNumber; }
        }

        public static string SearchBibleValidator2
        {

            get { return Resource.SearchBibleValidator2; }
        }

        public static string SearchBookValidator
        {

            get { return Resource.SearchBookValidator; }
        }

        public static string VerseValidator
        {

            get { return Resource.VerseValidator; }
        }

        public static string DoctorsTitle
        {

            get { return Resource.DoctorsTitle; }
        }

        public static string DateTitle
        {

            get { return Resource.DateTitle; }
        }

        public static string MyDates
        {

            get { return Resource.MyDates; }
        }

        public static string ShedulersAvailable
        {

            get { return Resource.ShedulersAvailable; }
        }

        public static string ChooseTime
        {

            get { return Resource.ChooseTime; }
        }

        public static string DateButton
        {

            get { return Resource.DateButton; }
        }

        public static string WorkDaysAvailable
        {

            get { return Resource.WorkDaysAvailable; }
        }

        




    }

}
