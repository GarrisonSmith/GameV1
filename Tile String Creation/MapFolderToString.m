clear global;
clear;

cur = "Tile String Creation\area_maps\";
foo = dir(cur);
for bar = foo'
    if bar.isdir && bar.name ~= "." && bar.name ~= ".."
        mapFolder = dir(string(bar.folder)+"\"+string(bar.name)+"\*.png");
        
        mapString = "";
        mapString = get_maps_string(mapFolder);
        
        textFileName = cur+string(bar.name)+".txt";
        fileID = fopen(textFileName,'w');
        fprintf(fileID, mapString);
        clear global;
    end
end

function foo = get_maps_string(mapFolder)
    global uniqueTiles %contains the 64x64 pixel data of all unique tiles. Index corresponds to tileIdentities.
    global tileIdentities %contains the assigned/generated name for all unique tiles. Index corresponds to uniqueTiles.
    tile_sets = dir("Tile String Creation\tile_sets\*.png");
    area_maps = mapFolder;
    uniqueTiles(1:128,1:64,1:3) = imread("squares.png");
    tileIdentities = ["","";"",""];
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
        get_tiles_from_set(tileSet,char(tileSetFile.name));
    end
    index = 1;
    for tileMapFile = area_maps'
        mapString(index) = "";
        mapString(index) = mapString(index) + "<" +tileMapFile.name(end-4:end-4) + ">";
        tileMap = imread(string(tileMapFile.folder)+"\"+string(tileMapFile.name));
        mapLayer = construct_map_string(tileMap);
        mapString(index) = mapString(index) + mapLayer;
        index = index + 1;
    end
    mapString = remove_empty_rows(mapString);
    foo= "";
    for index = 1:length(mapString) 
        foo = foo + mapString(index);
    end
end

function get_tiles_from_set(tileSet, name) %adds all the unique tiles to uniqueTiles
    [rows, columns, ~] = size(tileSet);
    for i=1:64:rows
        for j=1:64:columns
            add_unique_tile(tileSet(i:i+63,j:j+63,1:3), name(1:end-4), "("+string(j-1)+","+string(i-1)+")");
        end
    end
end

function add_unique_tile(new, name, coordinates) %add tiles only if a duplicate hasnt already been added.
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

function add_tile_identity(name, coordinates) %adds corresponding tile identity to the unique tile.
    global tileIdentities
    [rows, ~] = size(tileIdentities);
    tileIdentities(rows+1,1:2) = [name, coordinates];
end

function foo = construct_map_string(tileMap) %generates the map string by interating through the 64x64 tiles in the provided image and finding their unique tile and tile identity.
    foo = "";
    [rows, columns, ~] = size(tileMap);
    for i=1:64:rows
        for j=1:64:columns
            foo = foo+get_tile_string(tileMap(i:i+63,j:j+63,1:3))+":"; %splits columns
        end
        foo = foo + ";"; %splits rows
    end
            
end

function foo = get_tile_string(tile) %gets the provided tile's string from tileIdentities.
    global uniqueTiles    
    global tileIdentities
    [rows, ~, ~] = size(uniqueTiles);
    index = 1;
    for i=1:64:rows
        if tile == uniqueTiles(i:i+63,1:64,1:3)
            foo = tileIdentities(index,1)+tileIdentities(index,2);
            break;
        end
        index=index+1;
    end
end

function foo = remove_empty_rows(mapString) %removes any empty rows from the bottom of the string.
    
    lengthDif = intmax;
    for index=1:1:length(mapString)
        layerString = mapString(index);
        
        for i=1:1:strlength(layerString)
            empty = true;
            if(layerString{1}(i) == ';')
                for j = i:1:strlength(layerString)
                    if(layerString{1}(j) ~= ':' && layerString{1}(j) ~= ';')
                        empty = false;
                        break;
                    end
                end
            
                if (empty)
                    if (lengthDif > (strlength(layerString)-i))
                        lengthDif = (strlength(layerString)-i);
                    end
                    break;
                end
            end
        end
    end
    
    for index=1:1:length(mapString)
        layerString = mapString(index);
        layerString = layerString{1}(1:(strlength(layerString)-lengthDif));
        mapString(index) = layerString;
    end
    foo = mapString;
end