namespace Utils.Common {
    public class ReferenceableVariable<T> {
        public T Value { get => value; set => this.value = value; }
        public T StartValue { get; }
        private T value;

        public ReferenceableVariable(T startValue) {
            value = startValue;
            StartValue = startValue;
        }
    }
}
