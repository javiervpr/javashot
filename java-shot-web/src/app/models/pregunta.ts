import { Respuesta } from './respuesta';

export class Pregunta {
    constructor(
        public preguntaID?: string,
        public titulo?: string,
        public fechaRegistro?: Date,
        public contestada?: boolean,
        public contestadaCorrectamente?: boolean,
        public partidaPreguntaID?: string,
        public respuestas?: Respuesta[],
        public explicacionRespuesta?: string
    ) {}
}
