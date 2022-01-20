```
Camera:
    Props:
        [✓] Position/Translation: Vector2
        [✓] Scale: Vector2
        [✓] Viewport Size (World Units in Screen): Vector2
        [✓] Pixel Size: Vector2
        [✓] Bounds (min/max): tuple[Vector2, Vector2] (computed)
        [✓] Center: Vector2 (computed)
    Methods:
        [✓] Project/Unproject:
            [✓] Individual (x/y) Coords
            [✓] Coord Pairs
            [✓] Rects
        [✓] Translate
        [✓] Set Scale
        [✓] Resize Viewport
        [✓] Resize Screen
```

```
Entity:
    Props:
        [ ] Position: Vector2
        [ ] Size: Vector2
        [ ] Center: Vector2
        [ ] Sprite: Sprite
        [ ] Rect/Collider: Rect (computed)
    Methods:
        [ ] Move - return dict of collisions
        [ ] Render (calls Sprite.Render)
        [ ] Update
```

```
Tile:
    Props:
        [✓] Position: Vector2
        [ ] Size: Vector2
        [✓] Type: String
        [ ] Rect: Rect (computed)
    Methods:
        [ ] Render
```

```
Tilemap:
    Props:
        [✓] tiles: dict[tuple[int, int], dict[int, Tile]]
    Methods:
        [✓] Get slices (x,y) (All layers at position)
        [✓] Set/Get tiles (x,y,z)
        [ ] Get Visible Tiles
        [✓] Save/Load From File
```
