public class TiroData
{
    public float tiempo;      // Tiempo del tiro
    public float angulo;      // Ángulo del tiro
    public float velocidad;   // Velocidad del tiro
    public float distancia;   // Distancia alcanzada
    public float altura;      // Altura alcanzada

    
    public TiroData(float tiempo, float angulo, float velocidad, float distancia, float altura)
    {
        this.tiempo = tiempo;
        this.angulo = angulo;
        this.velocidad = velocidad;
        this.distancia = distancia;
        this.altura = altura;
    }
}

