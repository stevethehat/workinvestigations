import {Canvas, IPoint} from '@/util/canvas/canvas';

import Line from '@/util/canvas/line';
import Dot from '@/util/canvas/dot';

export interface IBoxDefinition {
    x: number;
    y: number;
    Width: number;
    Height: number;
    LineWidth: number;
    LineColor: string;
    Title: string;
}

export class Box {
    protected Canvas: Canvas;
    protected Anchors: { [id: string]: IPoint};
    protected Definition!: IBoxDefinition;
    constructor(canvas: Canvas, definition: IBoxDefinition) {
        this.Canvas = canvas;
        this.Anchors = {};
        if (definition) {
            this.draw(definition);
        }

    }

    public setupAnchors() {
        const definition = this.Definition;
        this.Anchors = {
            l: { x: definition.x , y: definition.y + (definition.Height / 2)},
            tl: { x: definition.x + (definition.Width * 0.25) , y: definition.y },
            tc: { x: definition.x + (definition.Width * 0.5) , y: definition.y },
            tr: { x: definition.x + (definition.Width * 0.75) , y: definition.y },
            r: { x: definition.x + definition.Width , y: definition.y + (definition.Height / 2) },
            br: { x: definition.x + (definition.Width * 0.75) , y: definition.y + definition.Height },
            bc: { x: definition.x + (definition.Width * 0.5) , y: definition.y + definition.Height },
            bl: { x: definition.x + (definition.Width * 0.25) , y: definition.y + definition.Height },
        };
    }

    public dot(position: string) {
        const anchor: IPoint = this.Anchors[position];

        this.Canvas.Context.beginPath();
        this.Canvas.Context.arc(anchor.x, anchor.y, 2, 0, 2 * Math.PI, true);
        this.Canvas.Context.stroke();
    }

    public draw(definition: IBoxDefinition) {
        this.Definition = definition;
        const context = this.Canvas.Context;

        this.setupAnchors();

        context.rect(definition.x, definition.y, definition.Width, definition.Height);
        context.lineWidth = definition.LineWidth;
        context.strokeStyle = definition.LineColor;
        context.stroke();

        context.font = '16pt Arial';
        context.fillText(definition.Title, definition.x + 4, definition.y + 24);

        /*
        this.dot("l");
        this.dot("tl");
        this.dot("tc");
        this.dot("tr");
        this.dot("r");
        this.dot("br");
        this.dot("bc");
        this.dot("bl");
        */
    }
    public join(box: Box, fromAnchor: string, toAnchor: string) {
        const from = this.Anchors[fromAnchor];
        const to = box.Anchors[toAnchor];
        const line = new Line(this.Canvas, from, to);

        const fromDot = new Dot(this.Canvas, from);
        const toDot = new Dot(this.Canvas, to);

        return box;
    }
}
