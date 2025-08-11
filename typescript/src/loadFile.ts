import * as fs from 'fs';
import * as path from 'path';

// Function to convert stream to File-like object
export async function loadFile(filePath: string, mimeType: string = 'application/octet-stream'): Promise<File> {
  const stream = fs.createReadStream(filePath);
  const chunks: Buffer[] = [];

  // Collect stream data into a Buffer
  for await (const chunk of stream) {
    chunks.push(Buffer.isBuffer(chunk) ? chunk : Buffer.from(chunk));
  }

  const buffer = Buffer.concat(chunks);
  const fileName = path.basename(filePath);
  const stats = await fs.promises.stat(filePath);

  return new File(
    [buffer], 
    fileName, 
    {
      type: mimeType
    }
  );

}