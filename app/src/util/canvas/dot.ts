import {Canvas, IPoint} from '@/util/canvas/canvas';

export default class Dot {
    constructor(readonly canvas: Canvas, position: IPoint) {
        const context = canvas.context;

        context.beginPath();
        context.arc(position.x, position.y, 2, 0, 2 * Math.PI, true);
        context.stroke();

    }
}
