import {Canvas} from '@/util/canvas/canvas';
import {Box, IBoxDefinition} from '@/util/canvas/box';
import Grid from '@/util/canvas/grid';

interface AreasDictionary { [id: string]: Box; }
export default class MoneyFlow {
    protected canvas: Canvas;
    protected grid: Grid;
    protected areas!: AreasDictionary;

    constructor() {
        this.canvas = new Canvas('displayCanvas');

        this.grid = new Grid(this.canvas, 3, 9, 360, 80, 20, 30);
        this.setupAreas();
    }

    protected setupAreas() {
        const height: number = this.grid.height;
        const width: number = this.grid.width;
        this.areas = {
            capital: this.getBox(2, 0, 'Capital'),
            fixedAssets: this.getBox(2, 2, 'Fixed Assets'),
            currentAssets: this.getBox(2, 4, 'Current Assets'),
            sales: this.getBox(2, 6, 'Sales'),
            invoicing: this.getBox(1, 7, 'Invoicing'),
            turnover: this.getBox(0, 6, 'Turnover'),
            grossProfit: this.getBox(0, 5, 'Gross Profit'),
            expenses: this.getBox(0, 4, 'Expenses'),
            operatingProfit: this.getBox(0, 3, 'Operating Profit'),
            interest: this.getBox(0, 2, 'Interest'),
            tax: this.getBox(0, 1, 'Tax'),
            netProfit: this.getBox(0, 0, 'Net Profit'),
        };

        const areas: AreasDictionary = this.areas;

        areas.capital.join(areas.fixedAssets, 'bl', 'tl')
        .join(areas.currentAssets, 'bc', 'tc')
        .join(areas.sales, 'bc', 'tc')
        .join(areas.invoicing, 'bl', 'r')
        .join(areas.turnover, 'l', 'br')
        .join(areas.grossProfit, 'tc', 'bc')
        .join(areas.expenses, 'tc', 'bc')
        .join(areas.operatingProfit, 'tc', 'bc')
        .join(areas.interest, 'tc', 'bc')
        .join(areas.tax, 'tc', 'bc')
        .join(areas.netProfit, 'tc', 'bc');
    }

    protected getBox(hSlot: number, vSlot: number, title: string): Box {
        const x: number = this.grid.hSlots[hSlot];
        const y: number = this.grid.vSlots[vSlot];
        const width: number = this.grid.width;
        const height: number = this.grid.height;
        const definition: IBoxDefinition = {
            x, y,
            width, height,
            lineColor: 'black', lineWidth: 1,
            title,
        };
        const result: Box = new Box(this.canvas, definition);

        return result;
    }
}
