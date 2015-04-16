namespace FinalProject.Messaging
{
    public delegate void Callback();

    public delegate void Callback<A>(A parameterOne);

    public delegate void Callback<A, B>(A parameterOne, B parameterTwo);
}