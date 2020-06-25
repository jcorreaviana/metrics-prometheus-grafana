using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prometheus;
using src;
using src.request;

namespace PromGrafMetrics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly Counter contractsCounter;
        private readonly Histogram valueHistogram;
        // private readonly Summary summaryMetric;
        // private readonly Gauge gaugeMetric;
        // private readonly Counter valueCounter;
        // private readonly Counter valueCounterNoLabel;
        private readonly ILogger<ContractController> _logger;
        private readonly IContractApplication contractApplication;
        public ContractController(ILogger<ContractController> logger, IContractApplication contractApplication)
        {
            _logger = logger;
            this.contractApplication = contractApplication;

            contractsCounter = Metrics.CreateCounter("total_contracts",
            "Data about Contract API Data",
            new CounterConfiguration{ LabelNames = new[] {"uuid"} });

            valueHistogram = Metrics.CreateHistogram("contract_api_histogram",
            "Values of contracts in Contract API Histogram",
            new HistogramConfiguration{ LabelNames = new[] {"value_histogram"} });

            // gaugeMetric = Metrics.CreateGauge("contract_api_gauge",
            // "Values of contracts in Contract API Gauge",
            // new GaugeConfiguration{ LabelNames = new[] {"value_gauge"} });

            // summaryMetric = Metrics.CreateSummary("contract_api_summary",
            // "Values of contracts in Contract API Summary",
            // new SummaryConfiguration{ LabelNames = new[] {"value_summary"} });

            // valueCounter = Metrics.CreateCounter("value_counter",
            // "Values of contracts in Contract Counter",
            // new CounterConfiguration{ LabelNames = new[] {"value_counter"} });

            // valueCounterNoLabel = Metrics.CreateCounter("value_counter_no_label",
            // "Values of contracts in Contract Counter",
            // new CounterConfiguration{ LabelNames = new[] {"value_counter_no_label"} });

        }

        [ProducesResponseType(200)]
        [HttpPost("submitcontractobject")]
        public IActionResult submitcontractobject([FromBody] ContractRequest request)
        {
            var metric = Metrics.CreateCounter("contract_api",
            "Data about Contract API Data", new CounterConfiguration
            {
                LabelNames = new[] {"method", "data"}
            });

            metric.WithLabels(HttpContext.Request.Method, JsonConvert.SerializeObject(request));

            return Ok();
        }

        [ProducesResponseType(200)]
        [HttpPost("submitcontract")]
        public IActionResult submitcontract([FromBody] ContractRequest request)
        {
            var response = contractApplication.GenerateRandomResponse(request);

            valueHistogram.Observe((double)response.contractValue);

            // summaryMetric.Observe((double)response.contractValue);

            // gaugeMetric.WithLabels(response.contractValue.ToString());

            // valueCounter.WithLabels(response.contractValue.ToString());

            contractsCounter.WithLabels(response.contractId.ToString()).Inc();

            return Ok();
        }
    }
}
