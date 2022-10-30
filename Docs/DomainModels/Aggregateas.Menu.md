# Domain Model

## Menu

```csharp
class Menu
{
    Manu Create();
    void AddDiner(Dinner dinner);
    void RemoveDinner(Dinner dinner);
    void UpdateSection(MenuSection menuSection);
}
```

```json
{
    "id":"00000000-0000-0000-0000-000000000000",
    "name":"Yummy Menu",
    "decription":"A menu with yummy food",
    "averageRating":3.5
    "sections":[
        {
            "id":"00000000-0000-0000-0000-000000000000",
            "name":"Appetizers"
            "description":"Starters",
            "items":[
                {
                    "id":"00000000-0000-0000-0000-000000000000",
                    "name":"Fried Pickes",
                    "decription":"Deep fried pickles"
                }
            ]
        }
    ],
    "createdDateTime":"2020-10-10 10:10:10",
    "updatedDateTime":"2020-10-10 10:10:10",
    "hostId":"00000000-0000-0000-0000-000000000000",
    "dinerIds":[
        "00000000-0000-0000-0000-000000000000",
        "00000000-0000-0000-0000-000000000000"
    ],
    "menuReviewIds":[        
        "00000000-0000-0000-0000-000000000000",
        "00000000-0000-0000-0000-000000000000"
    ],
}
```
