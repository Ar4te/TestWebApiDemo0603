﻿using Mapster;

namespace Models.MapsterConfig;

public class MapsterConfig
{
    public static TypeAdapterConfig SetMapsterConfig(TypeAdapterConfig config)
    {
        config.NewConfig<TestDto, Test>().Map(t => t.Id, td => td.Id + 1);
        return config;
    }
}
