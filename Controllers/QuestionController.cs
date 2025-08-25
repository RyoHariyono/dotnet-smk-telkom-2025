using Microsoft.AspNetCore.Mvc;

namespace dotnet_smk_telkom_2025.Controllers;

[ApiController]
[Route("questions")]
public class QuestionController : ControllerBase
{
  private readonly ILogger<QuestionController> _logger;

  public QuestionController(
    ILogger<QuestionController> logger
  )
  {
    _logger = logger;
  }


  /* ------------------------------- QUESTION 1 ------------------------------- */
  /* 
    Problem: A frontend developer accidentally passes "abc" instead of a number 
    Fix this with proper error handling.
    Suggestion: Use `BadRequest` if input is not a number.
  */
  [HttpGet("question-1")]
  public IActionResult Question1(
    [FromQuery] string discount
  )
  {
    if (!int.TryParse(discount, out int discountValue))
    {
      return BadRequest(new { message = "Discount must be a valid number" });
    }
    // int discountValue = int.Parse(discount); // <--- Crashes if input is not a number

    int price = 1000;
    int finalPrice = price - discountValue;

    return Ok(new
    {
      originalPrice = price,
      discount = discountValue,
      finalPrice
    });
  }

  /* ------------------------------- QUESTION 2 ------------------------------- */
  /* 
    Problem: Name is null because it came from an external source.
    App crashes. How would you fix it?
    Suggestion: Use `BadRequest` if you want to give validation feedback to the user.
  */
  public new class User
  {
    public string Name { get; set; }
    public string? Email { get; set; }

    public string GetInitial()
    {
      return Name.Substring(0, 1).ToUpper();
    }

    //! DONT CHANGE THIS FUNCTION
    public static User GetFromExternalSource()
    {
      return new User
      {
        Name = null,
      };
    }
  }

  [HttpGet("question-2")]
  public IActionResult Question2()
  {
    //! DONT CHANGE THIS LINE
    var user = User.GetFromExternalSource(); // <--Simulate missing data

    if (string.IsNullOrEmpty(user.Name))
    {
      return BadRequest(new { message = "Name is required" });
    }

    return Ok(new
    {
      userInitial = user.GetInitial()
    });
  }

  /* ------------------------------- QUESTION 3 ------------------------------- */
  /* 
    Problem: Discount logic is wrong. It gives higher prices for higher discounts.
    Fix the formula.
  */
  public class Question3RequestDto
  {
    public int Price { get; set; }
    public int DiscountPercent { get; set; }
  }

  [HttpPost("question-3")]
  public IActionResult Question3(
    Question3RequestDto requestDto
  )
  {
    // TODO: Business rule: apply discount based on % of price
    int discount = (requestDto.Price * requestDto.DiscountPercent) / 100; // <-- Wrong formula!
    int finalPrice = requestDto.Price - discount;

    return Ok(new
    {
      originalPrice = requestDto.Price,
      discountPercent = requestDto.DiscountPercent,
      discount,
      finalPrice
    });
  }

  /* ------------------------------- QUESTION 4 ------------------------------- */
  /*
    Problem: Complete the missing discount logic so members and bulk buyers get correct prices.
  */
  public class Order
  {
    public double Price { get; set; }
    public int Quantity { get; set; }
    public bool IsMember { get; set; }

    public double OriginalTotalPrice => this.Price * this.Quantity;

    public Order(
      Question4RequestDto requestDto
    )
    {
      this.Price = requestDto.Price;
      this.Quantity = requestDto.Quantity;
      this.IsMember = requestDto.IsMember;
    }

    public double CalculateFinalPrice()
    {
      double total = this.Price * this.Quantity;

      // Members get 10% discount
      if (this.IsMember)
      {
        total *= 0.9;  // TODO: apply member discount
      }

      // If buying more than 5 books, apply additional 5% discount
      if (this.Quantity > 5)
      {
        total *= 0.95;// TODO: apply bulk discount
      }

      return total;
    }
  }

  public class Question4RequestDto
  {
    public double Price { get; set; }
    public int Quantity { get; set; }
    public bool IsMember { get; set; }
  }

  [HttpPost("question-4")]
  public IActionResult Question4(
    [FromBody] Question4RequestDto requestDto
  )
  {
    var order = new Order(requestDto);
    double finalPrice = order.CalculateFinalPrice();

    return Ok(new
    {
      originalPrice = order.OriginalTotalPrice,
      isMember = order.IsMember,
      quantity = order.Quantity,
      finalPrice
    });
  }

  /* ------------------------------- QUESTION 5 ------------------------------- */
  /*
    Task: Complete the late fee calculation based on rules
  */
  public class Library
  {
    public double CalculateLateFee(int daysLate)
    {
      double fee = 0;

      // TODO: calculate fee correctly
      // First 5 days: 1000 per day
      // After 5 days: 2000 per day

      if (daysLate <= 5)
      {
        fee = daysLate * 1000;
      }
      else
      {
        fee = (5 * 1000) + ((daysLate - 5) * 2000);
      }

      return fee;
    }
  }

  [HttpGet("question-5")]
  public IActionResult Question5(
    [FromQuery] int daysLate
  )
  {
    var library = new Library();
    double lateFee = library.CalculateLateFee(daysLate);

    return Ok(new
    {
      daysLate,
      lateFee
    });
  }
}
