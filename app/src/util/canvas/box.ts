import {Canvas, IPoint} from '@/util/canvas/canvas';

import Line from '@/util/canvas/line';
import Dot from '@/util/canvas/dot';

export interface IBoxDefinition {
    x: number;
    y: number;
    width: number;
    height: number;
    lineWidth: number;
    lineColor: string;
    title: string;
}

export class Box {
    protected anchors: { [id: string]: IPoint };
    protected definition!: IBoxDefinition;

    constructor(readonly canvas: Canvas, definition: IBoxDefinition) {
        this.anchors = {};
        if (definition) {
            this.draw(definition);
        }

    }

    public setupAnchors() {
        const definition: IBoxDefinition = this.definition;
        this.anchors = {
            l: { x: definition.x , y: definition.y + (definition.height / 2)},
            tl: { x: definition.x + (definition.width * 0.25) , y: definition.y },
            tc: { x: definition.x + (definition.width * 0.5) , y: definition.y },
            tr: { x: definition.x + (definition.width * 0.75) , y: definition.y },
            r: { x: definition.x + definition.width , y: definition.y + (definition.height / 2) },
            br: { x: definition.x + (definition.width * 0.75) , y: definition.y + definition.height },
            bc: { x: definition.x + (definition.width * 0.5) , y: definition.y + definition.height },
            bl: { x: definition.x + (definition.width * 0.25) , y: definition.y + definition.height },
        };
    }

    public dot(position: string) {
        const anchor: IPoint = this.anchors[position];

        this.canvas.context.beginPath();
        this.canvas.context.arc(anchor.x, anchor.y, 2, 0, 2 * Math.PI, true);
        this.canvas.context.stroke();
    }

    public draw(definition: IBoxDefinition) {
        this.definition = definition;
        const context = this.canvas.context;

        this.setupAnchors();

        context.rect(definition.x, definition.y, definition.width, definition.height);
        context.lineWidth = definition.lineWidth;
        context.strokeStyle = definition.lineColor;
        context.stroke();

        context.font = '16pt Arial';
        context.fillText(definition.title, definition.x + 4, definition.y + 24);

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
        const from: IPoint = this.anchors[fromAnchor];
        const to: IPoint = box.anchors[toAnchor];
        const line: Line = new Line(this.canvas, from, to);

        const fromDot: Dot = new Dot(this.canvas, from);
        const toDot: Dot = new Dot(this.canvas, to);

        return box;
    }
}
