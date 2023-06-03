using Microsoft.AspNetCore.Mvc;

namespace FurryFeast.Areas.Admin.Controllers {
    [Area("Admin")]
    public class MyStockControlController : Controller {
		public IActionResult StockArticles() {
			return View();
		}

		public IActionResult StockGroups() {
			return View();
		}

		public IActionResult StockImages() {
			return View();
		}

		public IActionResult StockMeasureUnits() {
			return View();
		}

		public IActionResult StockSuppliers() {
			return View();
		}

		public IActionResult StockSuppliersGroups() {
			return View();
		}

		public IActionResult StockWarehouseGroups() {
			return View();
		}

		public IActionResult StockWarehouses() {
			return View();
		}
	}
}
