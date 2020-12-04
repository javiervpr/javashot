export interface RespuestaAPI<T> {
    codigo: number ;
    fechaHora: Date ;
    mensaje: string ;
    data: T ;
}
