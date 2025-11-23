using Microsoft.Extensions.Logging;
﻿using MiningTradingMobileApp.Converters;
﻿using MiningTradingMobileApp.Services;
﻿using MiningTradingMobileApp.ViewModels;
﻿
﻿namespace MiningTradingMobileApp
﻿{
﻿    public static class MauiProgram
﻿    {
﻿        public static MauiApp CreateMauiApp()
﻿        {
﻿            var builder = MauiApp.CreateBuilder();
﻿            builder
﻿                .UseMauiApp<App>()
﻿                .ConfigureFonts(fonts =>
﻿                {
﻿                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
﻿                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
﻿                });
﻿
﻿            builder.Services.AddSingleton<NullToBoolConverter>();
            builder.Services.AddSingleton<NotNullToBoolConverter>();

            // Services
            builder.Services.AddSingleton<ITradeService, MockTradeService>();
            builder.Services.AddSingleton<IMarginService, MockMarginService>();
            builder.Services.AddSingleton<IMarginRequestService, MockMarginRequestService>();
            builder.Services.AddSingleton<IPaymentService, MockPaymentService>();
            builder.Services.AddSingleton<IWarrantService, MockWarrantService>();
            builder.Services.AddSingleton<IMineralListingService, MockMineralListingService>();
            builder.Services.AddSingleton<ISettlementService, MockSettlementService>();

            // ViewModels
            builder.Services.AddTransient<ContractDetailsViewModel>();
            builder.Services.AddTransient<MarginDisplayViewModel>();
            builder.Services.AddTransient<PaymentFormViewModel>();
            builder.Services.AddTransient<TradeListViewModel>();
            builder.Services.AddTransient<WarrantDetailsViewModel>();
            builder.Services.AddTransient<MineralListingListViewModel>();
            builder.Services.AddTransient<SellerTradeListViewModel>();
            builder.Services.AddTransient<SellerWarrantListViewModel>();
            builder.Services.AddTransient<SettlementListViewModel>();
﻿
﻿#if DEBUG
﻿    		builder.Logging.AddDebug();
﻿#endif
﻿
﻿            return builder.Build();
﻿        }
﻿    }
﻿}
