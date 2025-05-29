using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeYou.Domain.Core.Models;

public class BaseSimpleDto
{
    public long Id { get; set; }
}