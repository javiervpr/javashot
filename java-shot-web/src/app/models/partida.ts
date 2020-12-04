import { Pregunta } from './pregunta';

export class Partida {
    constructor(
        public partidaID?: number,
        public usuarioID?: number,
        public fechaRegistro?: Date,
        public preguntas?: Pregunta[],
    ) {}
}
