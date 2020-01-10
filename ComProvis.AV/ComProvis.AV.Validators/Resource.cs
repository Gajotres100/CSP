namespace ComProvis.AV.Validators
{
    static class Resource
    {
        public static string ErrObjectIsNull { get => "{PropertyName} ne može biti null"; }
        public static string ErrCustomerExist { get => "Tvrtka već postoji u bazi"; }
        public static string ErrCustomerDontExist { get => "Tvrtka ne postoji u bazi"; }
        public static string ErrObjectIsNotValid { get => "{PropertyName} ne može biti null"; }
        public static string ErrObjectLength255 { get => "{PropertyName} mora biti manji od 255 znakova"; }
        public static string ErrProductDontExist { get => "Product ne postoji u bazi"; }
        public static string ErrUserDontExist { get => "User ne postoji u bazi"; }
        public static string ErrUserExist { get => "User već postoji u bazi"; }
        public static string RegexEmail { get => @"^[_a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,4})$"; }
    }
}
