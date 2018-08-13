import {Canvas, Point} from './canvas';

export default class Dot {
    protected canvas: Canvas;
    constructor(canvas: Canvas, position: Point) {
        this.canvas = canvas;
        const context = canvas.Context;

        context.beginPath();
        context.arc(position.x, position.y, 2, 0, 2 * Math.PI, true);
        context.stroke();

    }
}
