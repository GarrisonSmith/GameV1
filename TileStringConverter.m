clear global;
clear;
global uniqueTiles
global tileIdentities
folder = "E:\Matlab Work Spaces\Tile Creation\area_maps";
tile_sets = dir("E:\Matlab Work Spaces\Tile Creation\tile_sets\*.png");
area_maps = dir("E:\Matlab Work Spaces\Tile Creation\area_maps\*.png");
uniqueTiles(1:64,1:64,1:3) = imread("black_square.png");
tileIdentities = ["BLA","CK"];
for tileSetFile = tile_sets'
    tileSet = imread(string(tileSetFile.folder)+"\"+string(tileSetFile.name));
    [rows, columns, ~] = size(tileSet);
    if mod(rows, 64) ~= 0
        disp("rows wasnt a factor of 64.")
        break;
    end
    if mod(columns, 64) ~= 0
        disp("columns wasnt a factor of 64.")
        break;
    end
    get_tiles_from_set(tileSet,string(tileSetFile.name));
end
for tileMapFile = area_maps'
    textFileName = char(tileMapFile.name); 
    textFileName = "E:\Matlab Work Spaces\Tile Creation\area_maps\"+textFileName(1:end-4)+"1.txt";
    fileID = fopen(textFileName,'w');
    mapString = "";
    tileMap = imread(string(tileMapFile.folder)+"\"+string(tileMapFile.name));
    mapString = mapString+construct_map_string(tileMap);
    fprintf(fileID, mapString);
end

function get_tiles_from_set(tileSet, name)
    [rows, columns, ~] = size(tileSet);
    for i=1:64:rows
        for j=1:64:columns
            add_unique_tile(tileSet(i:i+63,j:j+63,1:3), name, "("+string(i)+","+string(j)+")");
        end
    end
end

function add_unique_tile(new, name, coordinates) %add tiles only if a duplicate hasnt already been added
    global uniqueTiles
    [rows, ~, ~] = size(uniqueTiles);
    unique = true;
    for k=1:64:rows
        if(new == uniqueTiles(k:k+63,1:64,1:3))
            unique = false;
            break;
        end
    end
    if(unique)
        uniqueTiles(rows+1:rows+64,1:64,1:3) = new;
        add_tile_identity(name, coordinates);
    end
end

function add_tile_identity(name, coordinates)
    global tileIdentities
    [rows, ~] = size(tileIdentities);
    tileIdentities(rows+1,1:2) = [name, coordinates];
end

function foo = construct_map_string(tileMap)
    foo = "";
    [rows, columns, ~] = size(tileMap);
    for i=1:64:rows
        for j=1:64:columns
            foo = foo+get_tile_string(tileMap(i:i+63,j:j+63,1:3))+",";
        end
        foo = foo+";";
    end
            
end

function foo = get_tile_string(tile)
    global uniqueTiles    
    global tileIdentities
    [rows, columns, ~] = size(uniqueTiles);
    index = 1;
    for i=1:64:rows
        if tile == uniqueTiles(i:i+63,1:64,1:3)
            foo = tileIdentities(index,1)+tileIdentities(index,2);
            break;
        end
        index=index+1;
    end
end
