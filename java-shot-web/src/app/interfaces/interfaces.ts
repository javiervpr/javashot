export interface Partida {
    partidaID?: number;
    usuarioID?: number;
    preguntas?: Pregunta[];
}

export interface Pregunta {
    tituloPregunta?: string;
    contestadaCorrectamente?: boolean;
    respuestas?: Respuesta[];
}

export interface Respuesta {
    contenidoRespuesta?: string;
    correcta?: boolean;
}
