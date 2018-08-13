import {Canvas} from '@/util/canvas/canvas';

export default class Grid {
    public vSlots: number[];
    public hSlots: number[];

    constructor(
        readonly canvas: Canvas,
        readonly horrizontal: number, readonly vertical: number,
        readonly width: number, readonly height: number,
        readonly hMargin: number, readonly vMargin: number) {

        this.vSlots = [];
        this.hSlots = [];

        this.setupHSlots();
        this.setupVSlots();
    }

    public setupHSlots() {
        const totalWidth: number = (this.horrizontal * this.width) + ((this.horrizontal - 2) * this.hMargin);
        const margins: number = (this.canvas.displayCanvas.width - totalWidth) / 2;

        let x: number = margins;
        for (let i = 0; i < this.horrizontal; i++) {
            this.hSlots[i] = x;
            x = x + this.width + this.hMargin;
        }
    }

    public setupVSlots() {
        // debugger;
        const totalHeight: number = (this.vertical * this.height) + ((this.vertical - 1) * this.vMargin);
        const margins: number = (this.canvas.displayCanvas.height - totalHeight) / 2;

        let y: number = margins;
        for (let i = 0; i < this.vertical; i++) {
            this.vSlots[i] = y;
            y = y + this.height + this.vMargin;
        }
    }
}
