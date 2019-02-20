export class Parse{
    private _block: Block;
    constructor(block: string[]){
        this._block = new Block(block);
    }
    parseBlock() {
        let chunk = this._block.getNextChunk();
        while('' !== chunk){
            // process
            //console.log(chunk);

            chunk = this._block.getNextChunk();
        }
        return {};
    }

}

class Block{
    private _block: string[];
    private _line: string | undefined = '';
    constructor(block: string[]){
        this._block = block;
        this.advanceLine();
    }

    advanceLine(){
        this._line = this._block.shift();
    }
    getNextChunk(): string{
        let chunk = '';
        let chunkEnd = 0;

        if(undefined !== this._line){
            this._line = this._line.trimLeft();

            if('"' === this._line.substr(0, 1)){
                chunkEnd = this._line.indexOf('"', 1) +1;
            } else {
                chunkEnd = this._line.indexOf(' ');
            }

            if(-1 !== chunkEnd){
                chunk = this._line.substr(0, chunkEnd);
                this._line = this._line.substr(chunkEnd);
            } else {
                if('' !== this._line){
                    chunk = this._line;
                    this.advanceLine();
                } else {
                    this.advanceLine();
                    chunk = this.getNextChunk();
                }
            }
        }
        return chunk;
    }
}

/*


        public void JumpChunks(int howMany)
        {
            for(int i = 0;i < howMany; i++)
            {
                GetChunk();
            }
        }

        public string GetChunk(string chunkSeperator = " ")
        {
            string result = null;
            int chunkEnd = 0;
            Line = Line.TrimStart();
            if (Line.StartsWith("\""))
            {
                chunkEnd = Line.IndexOf("\"", 1) + 1;
            }
            else
            {
                chunkEnd = Line.IndexOf(chunkSeperator);
            }

            if (-1 != chunkEnd)
            {
                result = Line.Substring(0, chunkEnd);
                Line = Line.Substring(chunkEnd);
            }
            else
            {
                result = Line;
                Line = "";
            }
            ChunkIndex++;
            return result;
        }
*/